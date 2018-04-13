using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mewcotol_Com.Message
{

    /// <summary>
    /// 注意 索引寄存器 其他类处理 包括应答类 现在没时间处理
    /// </summary>
    public class RdRequest : AbstractMewMessage, IMewRequest
    {
        public RdRequest()
        {
            FunctionCode = "RD";
            MsgCmdStart = "#";
        }
        
        public RdRequest (byte stationid) : base (stationid)
        {
            FunctionCode = "RD";
            MsgCmdStart = "#";
        }
        public void ValidateResponse (IMewMsg response)
        {
            if (response.FunctionCode != "RD")
            {
                string msg = "paramter is invalid";
                throw new Exception (msg);
            }
        }
        
        public void CreateRequestData (string type, int startAddr, int endAddr)
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
            Data = msg.ToString();
        }
        
        public override int MinimumFrameSize { get { return 8; } }
        
        protected override void InitializeUnique (byte[] frame)
        {
            return;
        }
        
    }
}
