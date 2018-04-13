namespace Mewcotol_Com.Message
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    
    public class MewException : Exception
    {
        private const string SlaveAddressPropertyName = "SlaveAdress";
        private const string FunctionCodePropertyName = "FunctionCode";
        private const string SlaveExceptionCodePropertyName = "SlaveExceptionCode";
        
        private readonly MewExceptionResponse _MewExceptionResponse;
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="MewException" /> class.
        /// </summary>
        public MewException()
        {
        }
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="MewException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public MewException (string message)
        : base (message)
        {
        }
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="MewException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public MewException (string message, Exception innerException)
        : base (message, innerException)
        {
        }
        
        internal MewException (MewExceptionResponse MewExceptionResponse)
        {
            _MewExceptionResponse = MewExceptionResponse;
        }
        
        [SuppressMessage ("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Used by test code.")]
        internal MewException (string message, MewExceptionResponse MewExceptionResponse)
        : base (message)
        {
            _MewExceptionResponse = MewExceptionResponse;
        }
        
        
        
        /// <summary>
        ///     Gets a message that describes the current exception.
        /// </summary>
        /// <value>
        ///     The error message that explains the reason for the exception, or an empty string.
        /// </value>
        public override string Message
        {
            get
            {
                string responseString;
                responseString = _MewExceptionResponse != null ? string.Concat (Environment.NewLine, _MewExceptionResponse.ToString()) : string.Empty;
                return string.Concat (base.Message, responseString);
            }
        }
        
        
        
        /// <summary>
        ///     Gets the slave exception code, or 0.
        /// </summary>
        /// <value>The slave exception code.</value>
        public byte MewExceptionCode
        {
            get { return _MewExceptionResponse != null ? _MewExceptionResponse.MewExceptionCode : (byte) 0; }
        }
        
        
        
        
    }
}
