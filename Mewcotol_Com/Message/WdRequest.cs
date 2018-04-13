using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mewcotol_Com.Message
{
    public class WdRequest : AbstractMewMessage, IMewRequest
    {
        public WdRequest()
        {
            FunctionCode = "WD";
            MsgCmdStart = "#";
        }
        
        public WdRequest (byte stationid) : base (stationid)
        {
            FunctionCode = "WD";
            MsgCmdStart = "#";
        }
        public void ValidateResponse (IMewMsg response)
        {
            if (response.FunctionCode != "WD")
            {
                string msg = "paramter is invalid";
                throw new Exception (msg);
            }
        }
        
        public void CreateRequestData (string type, int startAddr, int endAddr, List<int> regs)
        {
            switch (type)
            {
                case "D":
                case "F":
                case "L":
                    break;
                    
                default:
                    {
                        throw new ArgumentException ("读取操作数据寄存器类型出错");
                    }
            }
            
            if (type.Length != 1 || startAddr < 0 || endAddr < startAddr)
            {
                throw new ArgumentException ("接点类型参数长度出错");
            }
            
            StringBuilder msg = new StringBuilder();
            msg.Append ($"{type}{startAddr:D5}{endAddr:D5}");
            
            for (var i = 0; i < regs.Count; i++)
            {
                msg.Append ($"{Convert.ToString(regs.ElementAt(i),16):D4}");
            }
            
            Data = msg.ToString();
        }
        
        public override int MinimumFrameSize { get { return 8; } }
        
        protected override void InitializeUnique (byte[] frame)
        {
            return;
        }
        
    }
}
