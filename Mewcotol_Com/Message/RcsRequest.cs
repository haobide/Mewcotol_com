using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mewcotol_Com.Message
{
    public class RcsRequest : AbstractMewMessage, IMewRequest
    {
        public RcsRequest()
        {
            FunctionCode = "RCS";
            MsgCmdStart = "#";
        }
        
        public RcsRequest (byte stationid) : base (stationid)
        {
            FunctionCode = "RCS";
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
        
        public void CreateRequestData (string type, string addr)
        {
            switch (type)
            {
                case "X":
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
            
            Data = type + addr;
        }
        
        public override int MinimumFrameSize { get { return 8; } }
        
        protected override void InitializeUnique (byte[] frame)
        {
            return;
        }
        
    }
}
