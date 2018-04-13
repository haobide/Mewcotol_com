using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mewcotol_Com.IO
{
    public interface IStreamResource : IDisposable
    {
        /// <summary>
        ///     Indicates that no timeout should occur.
        /// </summary>
        int InfiniteTimeout { get; }
        
        /// <summary>
        ///     Gets or sets the number of milliseconds before a timeout occurs when a read operation does not finish.
        /// </summary>
        int ReadTimeout { get; set; }
        
        string ReadLine();
        
        /// <summary>
        ///     Gets or sets the number of milliseconds before a timeout occurs when a write operation does not finish.
        /// </summary>
        int WriteTimeout { get; set; }
        
        /// <summary>
        ///     Purges the receive buffer.
        /// </summary>
        void DiscardInBuffer();
        
        void Write (byte[] buffer, int offset, int count);
        
        int Read (byte[] buffer, int offset, int count);
        
        
    }
}
