using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mewcotol_Com.Message
{
    public class WriteSuccessResponse : AbstractMewMessage, IMewMsg
    {
        public WriteSuccessResponse()
        {
        }
        public WriteSuccessResponse (byte stationid) : base (stationid)
        {
        }
        
        public override int MinimumFrameSize { get { return 8; } }
        
        protected override void InitializeUnique (byte[] frame)
        {
            string results = Encoding.ASCII.GetString (frame);
            FunctionCode = results.Substring (4, 2);
        }
        
    }
}
