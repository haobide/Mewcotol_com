using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mewcotol_Com.Message
{
    internal static class Mewcotol
    {
        /// <summary>
        /// function id
        /// </summary>
        public const string FuncReadSingleCoil  = "RCS";
        public const byte  FuncReadSingleCoilCode = 1;
        public const string FuncWriteSingleCoil = "WCS";
        public const byte FuncWriteSingleCoilCode = 2;
        public const string FuncReadMultiCoil = "RCP";
        public const byte FuncReadMultiCoilCode = 3;
        public const string FuncWriteMultiCoil = "WCP";
        public const byte FuncWriteMultiCoilCode = 4;
        public const string FuncReadWordCoil = "RCC";
        public const byte FuncReadWordCoilCode = 5;
        public const string FuncWriteWordCoil = "WCC";
        public const byte FuncWriteWordCoilCode = 6;
        public const string PresetWordCoil = "SC";
        public const byte PresetWordCoilCode = 7;
        public const string ReadDataArea = "RD";
        public const byte ReadDataAreaCode = 8;
        public const string WriteDataArea = "WD";
        public const byte WriteDataAreaCode = 9;
        public const string PresetDataArea = "SD";
        public const byte PresetDataAreaCode = 10;
        public const string ReadTimerArea = "RS";
        public const byte ReadTimerAreaCode = 11;
        public const string WriteTimerArea = "WS";
        public const byte WriteTimerAreaCode = 12;
        
        
        
        ///Contact Codes
        public const string ExternalInput = "X";
        public const string ExternalOutput = "Y";
        public const string InternalRelay = "R";
        public const string Timer = "T";
        public const string Counter = "C";
        public const string LinkRelay = "L";
        /////Data Codes
        public const string DataRegister = "D";
        public const string LinkRegister = "L";
        public const string FileRegister = "F";
        public const string PresetValue = "S";
        public const string ElapsedValue = "K";
        public const string IndexRegisterInput = "IX";
        public const string IndexRegisterOutput = "IY";
        public const string WordExternalInput = "WX";
        public const string WordExternalOutput = "WY";
        public const string WordInternalRelay = "WR";
        public const string WordLinkRelay = "WL";
        
        /// <summary>
        /// commend symbol
        /// </summary>
        public const string MsgHeader = "%";
        
        public const string MsgRequestCmdStart = "#";
        
        public const string MsgRePonseCmdStart = "$";
        
        public const string MsgRePonseErrorCmdStart = "!";
        
        ///others
        public const int DefaultRetries = 3;
        public const int DefaultWaitToRetryMilliseconds = 1000;
        public const int WaitRetryGreaterThanZero = 10;
        
        public const int MinimumFrameSize = 8;
        public const byte ExceptionOffset = 19;
        public const byte ExceptionOffsetMax = 68;
        public const byte BusyError = 53;
    }
}
