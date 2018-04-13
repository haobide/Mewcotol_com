using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mewcotol_Com.Message
{
    public interface IMewMsg
    {
        string MsgStart { get;}
        byte StationID { get; set; }
        string MsgCmdStart { set; get; }
        string FunctionCode { get; set; }
        string Data { set; get; }
        byte? BccCheck { get; set; }
        void Initialize (byte[] frame);
        
        byte[] BuildMessageFrame();
        
    }
}
