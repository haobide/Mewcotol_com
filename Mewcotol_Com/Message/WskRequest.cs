using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 写入定时器/计数器 结果值与过程值
/// </summary>
namespace Mewcotol_Com.Message
{
    public class WskRequest : AbstractMewMessage, IMewRequest
    {
        public WskRequest()
        {
            //FunctionCode = "WS";
            MsgCmdStart = "#";
        }
        
        public WskRequest (byte stationid, string functioncode) : base (stationid)
        {
            FunctionCode = functioncode;
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
        
        public void CreateRequestData (int startAddr, int endAddr, List<int> regs)
        {
            if (startAddr < 0 || endAddr < startAddr)
            {
                throw new ArgumentException ("接点类型参数长度出错");
            }
            
            StringBuilder msg = new StringBuilder();
            msg.Append ($"{startAddr:D4}{endAddr:D4}");
            
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
