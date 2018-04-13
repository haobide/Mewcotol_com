using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mewcotol_Com.Message
{
    public class MewExceptionResponse: AbstractMewMessage, IMewMsg
    {
    
        private static readonly Dictionary<byte, string> _exceptionMessages = CreateExceptionMessages();
        
        public MewExceptionResponse()
        {
        }
        
        public MewExceptionResponse (byte stationid,  byte exceptionCode)
        : base (stationid)
        {
            MewExceptionCode = exceptionCode;
        }
        
        public override int MinimumFrameSize
        {
            get { return 3; }
        }
        
        private byte _CurrentExceptionCode = 0;
        public byte MewExceptionCode
        {
            get { return _CurrentExceptionCode; }
            
            set { _CurrentExceptionCode = value;  }
        }
        
        
        public override string ToString()
        {
            string msg = _exceptionMessages.ContainsKey (MewExceptionCode)
                         ? _exceptionMessages[MewExceptionCode]
                         : "Unknown";
            return msg;
        }
        internal static Dictionary<byte, string> CreateExceptionMessages()
        {
            Dictionary<byte, string> messages = new Dictionary<byte, string> (26);
            ///Link System Error
            messages.Add (20, ExceptionResource.ExceptionUnknown);
            messages.Add (21, ExceptionResource.NACKError);
            messages.Add (22, ExceptionResource.WACKError);
            messages.Add (23, ExceptionResource.DuplicatePortError);
            messages.Add (24, ExceptionResource.TransmissionFormatError);
            messages.Add (25, ExceptionResource.HardwareError);
            messages.Add (26, ExceptionResource.UnitNoError);
            messages.Add (27, ExceptionResource.NOTSupportError);
            messages.Add (28, ExceptionResource.NoResponseError);
            messages.Add (29, ExceptionResource.BufferClosedError);
            messages.Add (30, ExceptionResource.TimeOut);
            ///Basic Procedure Error
            messages.Add (40, ExceptionResource.ProcedureBCCError);
            messages.Add (41, ExceptionResource.ProcedureFormatError);
            messages.Add (42, ExceptionResource.ProcedureNOTSupportError);
            messages.Add (43, ExceptionResource.ProcedureError);
            ///Processing System Error
            messages.Add (50, ExceptionResource.LinkSetError);
            messages.Add (51, ExceptionResource.SimultaneousOperatingError);
            messages.Add (52, ExceptionResource.TransmitDisableError);
            messages.Add (53, ExceptionResource.BusyError);
            ///Programmable Controller Application Error
            messages.Add (60, ExceptionResource.ParameterError);
            messages.Add (61, ExceptionResource.DataError);
            messages.Add (62, ExceptionResource.RegistrationError);
            messages.Add (63, ExceptionResource.ProgrammableControllerPLCModeError);
            messages.Add (65, ExceptionResource.ProtectError);
            messages.Add (66, ExceptionResource.AddressError);
            messages.Add (67, ExceptionResource.MissingDataError);
            return messages;
        }
        
        protected override void InitializeUnique (byte[] frame)
        {
            string results =  Encoding.ASCII.GetString (frame);
            Data = results.Substring (4, 2);
            byte exceptCode = Convert.ToByte (results.Substring (4, 2), 16);
            
            if (_exceptionMessages.ContainsKey (exceptCode))
            {
                throw new FormatException ("Unknown");
            }
            
            MewExceptionCode = exceptCode;
        }
        
        
    }
}
