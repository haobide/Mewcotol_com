using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mewcotol_Com.Message
{
    public class RccRequest : AbstractMewMessage, IMewRequest
    {
        public RccRequest()
        {
            FunctionCode = "RCC";
            MsgCmdStart = "#";
        }
        
        public RccRequest (byte stationid) : base (stationid)
        {
            FunctionCode = "RCC";
            MsgCmdStart = "#";
        }
        public void ValidateResponse (IMewMsg response)
        {
            if (response.FunctionCode != "RC")
            {
                string msg = "paramter is invalid";
                throw new Exception (msg);
            }
        }
        
        public void CreateRequestData (string type, int startAddr, int endAddr)
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
            Data = msg.ToString();
        }
        
        public override int MinimumFrameSize { get { return 8; } }
        
        protected override void InitializeUnique (byte[] frame)
        {
            return;
        }
        
    }
}
