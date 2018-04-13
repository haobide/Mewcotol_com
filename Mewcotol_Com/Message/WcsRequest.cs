using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mewcotol_Com.Message
{


    public class WcsRequest : AbstractMewMessage, IMewRequest
    {
        public WcsRequest()
        {
            FunctionCode = "WCS";
            MsgCmdStart = "#";
        }
        
        public WcsRequest (byte stationid) : base (stationid)
        {
            FunctionCode = "WCS";
            MsgCmdStart = "#";
        }
        public void ValidateResponse (IMewMsg response)
        {
            if (response.FunctionCode != "WC")
            {
                string msg = "paramter is invalid";
                throw new Exception (msg);
            }
        }
        
        public void CreateRequestData (string type, string addr, byte open)
        {
            switch (type)
            {
                case "Y":
                case "R":
                case "L":
                    break;
                    
                default:
                    {
                        throw new ArgumentException ("线圈类型出错");
                    }
            }
            
            if (type.Length != 1 || addr.Length != 4)
            {
                throw new ArgumentException ("接点类型参数长度出错");
            }
            
            Data = type + addr + Convert.ToString (open);
        }
        
        public override int MinimumFrameSize { get { return 15; } }
        
        protected override void InitializeUnique (byte[] frame)
        {
            return;
        }
        
    }
}
