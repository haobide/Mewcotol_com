using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mewcotol_Com.Message
{
    public interface IMewRequest: IMewMsg
    {
    
        /// <summary>
        ///     Validate the specified response against the current request.
        /// </summary>
        void ValidateResponse (IMewMsg response);
        
    }
}
