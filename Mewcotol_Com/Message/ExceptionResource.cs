using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mewcotol_Com.Message
{
    internal static class ExceptionResource
    {
        /// <summary>
        /// Link System Error
        /// </summary>
        public const string ExceptionUnknown = "Undefined";
        public const string NACKError = "Either the remote unit was in incorrectly recognized or a data error occurred";
        public const string WACKError = "The receive buffer of the remote unit is full";
        public const string DuplicatePortError = "The remote unit was set with the same unit no.(01 to 16) as the local unit";
        public const string TransmissionFormatError = "Attempt was made to send data that does not match the transmission format.Either a frame overflow or data error occurred";
        public const string HardwareError = "Transmission system hardware stops operating normally";
        public const string UnitNoError = "The unit no. of the remote unit was set to a number other than 01 to 63";
        public const string NOTSupportError = "Frame overflow at the receiving side. An instance where an attempt was made to send different frame lengths between different models.";
        public const string NoResponseError = "Remote does not exist. (time out)";
        public const string BufferClosedError = "Attempt was made to send or receive in the buffer closed state.";
        public const string TimeOut = "Transmit disable state continues";
        /// <summary>
        /// Basic Procedure Error
        /// </summary>
        public const string ProcedureBCCError = "Transmission error occurred in command data";
        public const string ProcedureFormatError = "Command message that does not match the transmission format was sent";
        public const string ProcedureNOTSupportError = "An unsupported command was sent.A command was sent to an unsupported destination";
        public const string ProcedureError = "Another command was sent during the transmission request message standby state.";
        /// <summary>
        /// Processing System Error
        /// </summary>
        public const string LinkSetError = "A link no. that does not exist was set";
        public const string SimultaneousOperatingError = "The transmit buffer of the local unit was already full when a command was issued to the other unit";
        public const string TransmitDisableError = "cannot transmit to another unit";
        public const string BusyError = "Processing for another command when a command was received.";
        /// <summary>
        /// Programmable Controller Application Error
        /// </summary>
        public const string ParameterError = "A code without an area specification parameter or a code which cannot be used in the command was found. (X, Y, D, etc.) " +
                                             "A code with an illegal function specification parameter(0,1,2, etc.) was found.";
        public const string DataError = "Contact no., area no., data code format (BCD, hex, etc.), overflow, " +
                                        "underflow or range specification error.";
        public const string RegistrationError = "Excessive number of registrations or operation in unregistered state. (monitor registration, trace registration, etc.) When a registration overflow occurs," +
                                                "perform a registration reset";
        public const string ProgrammableControllerPLCModeError = "Operating mode of the PLC when a command was sent does not allow " +
                "the command to be processed";
        public const string ProtectError = "Write operation was performed to the program area or system register in the memory protect states.";
        public const string AddressError = "Address (program address, absolute address, etc.) data code format(BCD, hex,etc.), overflow, underflow" +
                                           " or range specification error.";
        public const string MissingDataError = "Read data does not exist. (Data not written with a comment registration was read.)";
    }
}
