using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Diagnostics;

namespace Mewcotol_Com.IO
{
    public class SeriesStream: IStreamResource
    {
        private const string LineEnd = "\r\n";
        private SerialPort m_serialPort;
        
        public SeriesStream (SerialPort serialPort)
        {
            Debug.Assert (serialPort != null, "Argument serialPort cannot be null.");
            m_serialPort = serialPort;
            m_serialPort.NewLine = LineEnd;
        }
        
        
        public int InfiniteTimeout
        {
            get { return SerialPort.InfiniteTimeout; }
        }
        
        
        public string ReadLine()
        {
            return  m_serialPort.ReadLine();
        }
        
        /// <summary>
        ///     Gets or sets the number of milliseconds before a timeout occurs when a read operation does not finish.
        /// </summary>
        public int ReadTimeout { get { return m_serialPort.ReadTimeout; } set { m_serialPort.ReadTimeout = value; } }
        
        /// <summary>
        ///     Gets or sets the number of milliseconds before a timeout occurs when a write operation does not finish.
        /// </summary>
        public int WriteTimeout { get { return m_serialPort.WriteTimeout; } set { m_serialPort.WriteTimeout = value; } }
        
        /// <summary>
        ///     Purges the receive buffer.
        /// </summary>
        public  void DiscardInBuffer()
        {
            m_serialPort.DiscardInBuffer();
        }
        
        public void DiscardOutBuffer()
        {
            m_serialPort.DiscardOutBuffer();
        }
        
        public  void Write (byte[] buffer, int offset, int count)
        {
            m_serialPort.Write (buffer, offset, count);
        }
        
        public int Read (byte[] buffer, int offset, int count)
        {
            return m_serialPort.Read (buffer, offset, count);
        }
        
        
        public void  Dispose()
        {
            Dispose (true);
            GC.SuppressFinalize (this);
        }
        
        protected virtual void Dispose (bool disposing)
        {
            if (disposing)
            {
                m_serialPort.Dispose();
                m_serialPort = null;
            }
        }
        
    }
}
