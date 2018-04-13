using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mewcotol_Com.Message
{
    using Mewcotol_Com.Utility;
    internal class MewMessageImpl
    {
    
        public MewMessageImpl()
        {
        }
        
        public MewMessageImpl (byte staionid)
        {
            StationID = staionid;
        }
        
        
        public string MsgStart { get { return "%"; } }
        private byte stationid = 0;
        public byte StationID
        {
            get { return stationid; }
            
            set
            {
                if (value > 100)
                {
                    throw new ArgumentOutOfRangeException ("station id out range 100");
                }
                
                stationid = value;
            }
        }
        public string MsgCmdStart { set; get;}
        public string FunctionCode { get; set; }
        public string Data { get; set; }
        public byte? BccCheck { get; set; }
        
        public const string NewLine = "\r\n";
        
        public byte[] MessageFrame
        {
            get
            {
                StringBuilder frame = new StringBuilder ("%");
                frame.AppendFormat ("{D2}{1}", StationID, MsgCmdStart);
                
                if (FunctionCode != null)
                {
                    frame.Append ($"{FunctionCode}");
                }
                
                if (Data != null)
                {
                    frame.Append ($"{Data}");
                }
                
                BccCheck = UtilityBase.GetBccByte (frame.ToString());
                string bcc = UtilityBase.ByteToHexStr (BccCheck.Value);
                frame.Append ($"{bcc}{NewLine}");
                return Encoding.ASCII.GetBytes (frame.ToString());
            }
        }
        
        public void Initialize (byte[] frame)
        {
            if (frame == null)
            {
                throw new ArgumentNullException (nameof (frame), "Argument frame cannot be null.");
            }
            
            if (frame.Length < Mewcotol.MinimumFrameSize)
            {
                string msg = $"Message frame must contain at least {Mewcotol.MinimumFrameSize} bytes of data.";
                throw new FormatException (msg);
            }
            
            string src = Encoding.ASCII.GetString (frame);
            StationID = Convert.ToByte (src.Substring (1, 2));
            MsgCmdStart = src.Substring (3, 1);
            BccCheck = Convert.ToByte (src.Substring (src.Length - 2, 2), 16);
        }
    }
}
