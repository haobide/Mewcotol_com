using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mewcotol_Com.Message
{
    public class WcpRequest : AbstractMewMessage, IMewRequest
    {
        public WcpRequest()
        {
            FunctionCode = "WCP";
            MsgCmdStart = "#";
        }
        
        public WcpRequest (byte stationid) : base (stationid)
        {
            FunctionCode = "WCP";
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
        
        public void CreateRequestData (List<MutilCoils> coils)
        {
            if (coils == null)
            {
                throw new ArgumentException ("接点类型参数长度出错");
            }
            
            StringBuilder msg = new StringBuilder();
            
            for (var i = 0; i < coils.Count; i++)
            {
                switch (coils.ElementAt (i).type)
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
                
                if (coils.ElementAt (i).offset > 8)
                {
                    throw new ArgumentException ("线圈编号超过范围出错");
                }
                
                msg.Append ($"{coils.ElementAt(i).type}{coils.ElementAt(i).addr}");
                
                if (coils.ElementAt (i).open.HasValue)
                {
                    msg.Append ($"{coils.ElementAt(i).open.Value}");
                }
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
