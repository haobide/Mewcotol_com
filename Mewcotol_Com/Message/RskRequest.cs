using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 读取定时器/计数器 结果值与过程值
/// </summary>
namespace Mewcotol_Com.Message
{
    public class RskRequest : AbstractMewMessage, IMewRequest
    {
        public RskRequest()
        {
            //FunctionCode = "RS";
            MsgCmdStart = "#";
        }
        
        public RskRequest (byte stationid, string funcCode) : base (stationid)
        {
            FunctionCode = funcCode;
            MsgCmdStart = "#";
        }
        public void ValidateResponse (IMewMsg response)
        {
            if (response.FunctionCode != FunctionCode)
            {
                string msg = "paramter is invalid";
                throw new Exception (msg);
            }
        }
        
        public void CreateRequestData (int startAddr, int endAddr)
        {
            if (startAddr < 0 || endAddr < startAddr)
            {
                throw new ArgumentException ("接点类型参数长度出错");
            }
            
            StringBuilder msg = new StringBuilder();
            msg.Append ($"{startAddr:D4}{endAddr:D4}");
            Data = msg.ToString();
        }
        
        public override int MinimumFrameSize { get { return 8; } }
        
        protected override void InitializeUnique (byte[] frame)
        {
            return;
        }
        
    }
}
