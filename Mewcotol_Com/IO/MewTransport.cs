namespace Mewcotol_Com.IO
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
    
    using Mewcotol_Com.Message;
    
    
    public abstract class MewTransport : IDisposable
    {
        private readonly object _syncLock = new object();
        private int _retries = Mewcotol.DefaultRetries;
        private int _waitToRetryMilliseconds = Mewcotol.DefaultWaitToRetryMilliseconds;
        private IStreamResource _streamResource;
        
        /// <summary>
        ///     This constructor is called by the NullTransport.
        /// </summary>
        internal MewTransport()
        {
        }
        
        internal MewTransport (IStreamResource streamResource)
        {
            Debug.Assert (streamResource != null, "Argument streamResource cannot be null.");
            _streamResource = streamResource;
        }
        
        /// <summary>
        ///     Number of times to retry sending message after encountering a failure such as an IOException,
        ///     TimeoutException, or a corrupt message.
        /// </summary>
        public int Retries
        {
            get { return _retries; }
            
            set { _retries = value; }
        }
        
        /// <summary>
        /// If non-zero, this will cause a second reply to be read if the first is behind the sequence number of the
        /// request by less than this number.  For example, set this to 3, and if when sending request 5, response 3 is
        /// read, we will attempt to re-read responses.
        /// </summary>
        public uint RetryOnOldResponseThreshold { get; set; }
        
        
        
        /// <summary>
        ///     Gets or sets the number of milliseconds the tranport will wait before retrying a message after receiving
        ///     an ACKNOWLEGE or SLAVE DEVICE BUSY slave exception response.
        /// </summary>
        public int WaitToRetryMilliseconds
        {
            get
            {
                return _waitToRetryMilliseconds;
            }
            
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException ("can't lower zero");
                }
                
                _waitToRetryMilliseconds = value;
            }
        }
        
        /// <summary>
        ///     Gets or sets the number of milliseconds before a timeout occurs when a read operation does not finish.
        /// </summary>
        public int ReadTimeout
        {
            get { return StreamResource.ReadTimeout; }
            
            set { StreamResource.ReadTimeout = value; }
        }
        
        /// <summary>
        ///     Gets or sets the number of milliseconds before a timeout occurs when a write operation does not finish.
        /// </summary>
        public int WriteTimeout
        {
            get { return StreamResource.WriteTimeout; }
            
            set { StreamResource.WriteTimeout = value; }
        }
        
        /// <summary>
        ///     Gets the stream resource.
        /// </summary>
        internal IStreamResource StreamResource
        {
            get { return _streamResource; }
        }
        
        
        
        internal virtual T UnicastMessage<T> (IMewMsg message)
        where T : IMewMsg, new ()
        {
            IMewMsg response = null;
            int attempt = 1;
            bool success = false;
            
            do
            {
                try
                {
                    lock (_syncLock)
                    {
                        Write (message);
                        bool readAgain;
                        
                        do
                        {
                            readAgain = false;
                            response = ReadResponse<T>();
                            var exceptionResponse = response as MewExceptionResponse;
                            
                            if (exceptionResponse != null)
                            {
                                if (readAgain)
                                {
                                    Debug.WriteLine ($"Received ACKNOWLEDGE slave exception response, waiting {_waitToRetryMilliseconds} milliseconds and retrying to read response.");
                                    Sleep (WaitToRetryMilliseconds);
                                }
                                
                                else
                                {
                                    throw new MewException (exceptionResponse);
                                }
                            }
                            
                            else
                                if (ShouldRetryResponse (message, response))
                                {
                                    readAgain = true;
                                }
                        }
                        while (readAgain);
                    }
                    
                    ValidateResponse (message, response);
                    success = true;
                }
                
                catch (MewException se)
                {
                    if (se.MewExceptionCode != Mewcotol.BusyError)
                    {
                        throw;
                    }
                    
                    if (attempt++ > _retries)
                    {
                        throw;
                    }
                    
                    Debug.WriteLine ($"Received SLAVE_DEVICE_BUSY exception response, waiting {_waitToRetryMilliseconds} milliseconds and resubmitting request.{se.Message}");
                    Sleep (WaitToRetryMilliseconds);
                }
                
                catch (Exception e)
                {
                    if (e is FormatException ||
                            e is NotImplementedException ||
                            e is TimeoutException ||
                            e is IOException)
                    {
                        Debug.WriteLine ($"{e.GetType().Name}, {(_retries - attempt + 1)} retries remaining - {e}");
                        
                        if (attempt++ > _retries)
                        {
                            throw;
                        }
                    }
                    
                    else
                    {
                        throw;
                    }
                }
            }
            while (!success);
            
            return (T) response;
        }
        
        internal virtual IMewMsg CreateResponse<T> (byte[] frame)
        where T : IMewMsg, new ()
        {
            byte resultSymbol = frame[3];
            IMewMsg response;
            const char fail = '!';
            
            // check for slave exception response else create message from frame
            if (resultSymbol == (byte) (fail))
            {
                response = MewMessageFactory.CreateModbusMessage<MewExceptionResponse> (frame);
            }
            
            else
            {
                response = MewMessageFactory.CreateModbusMessage<T> (frame);
            }
            
            return response;
        }
        
        internal void ValidateResponse (IMewMsg request, IMewMsg response)
        {
            // always check the function code and slave address, regardless of transport protocol
            if (request.StationID != response.StationID)
            {
                string msg = $"Response slave address does not match request. Expected {response.StationID}, received {request.StationID}.";
                throw new IOException (msg);
            }
            
            // message specific validation
            var req = request as IMewRequest;
            
            if (req != null)
            {
                req.ValidateResponse (response);
            }
            
            OnValidateResponse (request, response);
        }
        
        /// <summary>
        ///     Check whether we need to attempt to read another response before processing it (e.g. response was from previous request)
        /// </summary>
        internal bool ShouldRetryResponse (IMewMsg request, IMewMsg response)
        {
            // These checks are enforced in ValidateRequest, we don't want to retry for these
            if (request.StationID != response.StationID)
            {
                return false;
            }
            
            return OnShouldRetryResponse (request, response);
        }
        
        /// <summary>
        ///     Provide hook to check whether receiving a response should be retried
        /// </summary>
        internal virtual bool OnShouldRetryResponse (IMewMsg request, IMewMsg response)
        {
            return false;
        }
        
        /// <summary>
        ///     Provide hook to do transport level message validation.
        /// </summary>
        internal abstract void OnValidateResponse (IMewMsg request, IMewMsg response);
        
        
        internal abstract IMewMsg ReadResponse<T>()
        where T : IMewMsg, new ();
        
        
        
        internal abstract void Write (IMewMsg message);
        
        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose (bool disposing)
        {
            if (disposing)
            {
                _streamResource.Dispose();
            }
        }
        
        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose (true);
            GC.SuppressFinalize (this);
        }
        private static void Sleep (int millisecondsTimeout)
        {
            Task.Delay (millisecondsTimeout).Wait();
        }
    }
}
