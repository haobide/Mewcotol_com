using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mewcotol_Com.Message
{
    public class SdRequest : AbstractMewMessage, IMewRequest
    {
        public SdRequest()
        {
            FunctionCode = "SD";
            MsgCmdStart = "#";
        }
        
        public SdRequest (byte stationid) : base (stationid)
        {
            FunctionCode = "SD";
            MsgCmdStart = "#";
        }
        public void ValidateResponse (IMewMsg response)
        {
            if (response.FunctionCode != "SD")
            {
                string msg = "paramter is invalid";
                throw new Exception (msg);
            }
        }
        
        public void CreateRequestData (string type, int startAddr, int endAddr, int value)
        {
            switch (type)
            {
                case "D":
                case "F":
                case "L":
                    break;
                    
                default:
                    {
                        throw new ArgumentException ("复位数据寄存器类型出错");
                    }
            }
            
            if (type.Length != 1 || startAddr < 0 || endAddr < startAddr)
            {
                throw new ArgumentException ("接点类型参数长度出错");
            }
            
            StringBuilder msg = new StringBuilder();
            msg.Append ($"{type}{startAddr:D5}{endAddr:D5}");
            msg.Append ($"{Convert.ToString(value,16):D4}");
            Data = msg.ToString();
        }
        
        public override int MinimumFrameSize { get { return 6; } }
        
        protected override void InitializeUnique (byte[] frame)
        {
            return;
        }
        
    }
}
