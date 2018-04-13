using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mewcotol_Com.Message
{
    public abstract class AbstractMewMessage
    {
        private readonly MewMessageImpl _messageImpl;
        
        internal AbstractMewMessage()
        {
            _messageImpl = new MewMessageImpl();
        }
        internal AbstractMewMessage (byte staionid)
        {
            _messageImpl = new MewMessageImpl (staionid);
        }
        
        public string MsgStart { get { return "%"; }}
        
        public byte StationID
        {
            get { return _messageImpl.StationID; }
            
            set { _messageImpl.StationID = value; }
        }
        
        public string MsgCmdStart
        {
            set { _messageImpl.MsgCmdStart = value; }
            
            get { return _messageImpl.MsgCmdStart; }
        }
        public string FunctionCode
        {
            get { return _messageImpl.FunctionCode; }
            
            set { _messageImpl.FunctionCode = value; }
        }
        
        
        
        public byte? BccCheck { get { return _messageImpl.BccCheck; } set { _messageImpl.BccCheck = value; } }
        
        public byte[] MessageFrame
        {
            get { return _messageImpl.MessageFrame; }
        }
        
        public virtual string Data
        {
            get { return _messageImpl.Data; }
            
            set { _messageImpl.Data = value; }
        }
        
        public byte[] BuildMessageFrame()
        {
            return  _messageImpl.MessageFrame;
        }
        
        internal MewMessageImpl MessageImpl
        {
            get { return _messageImpl; }
        }
        
        public void Initialize (byte[] frame)
        {
            if (frame.Length < MinimumFrameSize)
            {
                string msg = $"Message frame must contain at least {MinimumFrameSize} bytes of data.";
                throw new FormatException (msg);
            }
            
            _messageImpl.Initialize (frame);
            InitializeUnique (frame);
        }
        
        public  virtual int MinimumFrameSize { get { return 8; } }
        
        protected abstract void InitializeUnique (byte[] frame);
    }
    
    
    
    
}
