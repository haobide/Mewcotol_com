using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mewcotol_Com.Message
{
    public class ReadSuccessResponse : AbstractMewMessage, IMewMsg
    {
        public ReadSuccessResponse()
        {
        }
        public ReadSuccessResponse (byte stationid) : base (stationid)
        {
        }
        
        
        
        protected override void InitializeUnique (byte[] frame)
        {
            string results = Encoding.ASCII.GetString (frame);
            int len = results.Length;
            FunctionCode = results.Substring (4, 2);
            Data = results.Substring (6, len - 8);
        }
        
    }
}
