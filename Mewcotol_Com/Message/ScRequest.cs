using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mewcotol_Com.Message
{
    public class ScRequest : AbstractMewMessage, IMewRequest
    {
        public ScRequest()
        {
            FunctionCode = "SC";
            MsgCmdStart = "#";
        }
        
        public ScRequest (byte stationid) : base (stationid)
        {
            FunctionCode = "SC";
            MsgCmdStart = "#";
        }
        public void ValidateResponse (IMewMsg response)
        {
            if (response.FunctionCode != "SC")
            {
                string msg = "paramter is invalid";
                throw new Exception (msg);
            }
        }
        
        public void CreateRequestData (string type, int startAddr, int endAddr, int value)
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
            
            if (type.Length != 1 || startAddr < 0 || endAddr < startAddr)
            {
                throw new ArgumentException ("接点类型参数长度出错");
            }
            
            StringBuilder msg = new StringBuilder();
            msg.Append ($"{type}{startAddr:D4}{endAddr:D4}");
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
