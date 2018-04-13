namespace Mewcotol_Com.Message
{
    using System;
    
    /// <summary>
    ///     Modbus message factory.
    /// </summary>
    public static class MewMessageFactory
    {
        /// <summary>
        ///     Minimum request frame length.
        /// </summary>
        private const int MinRequestFrameLength = 3;
        
        /// <summary>
        ///     Create a Modbus message.
        /// </summary>
        /// <typeparam name="T">Modbus message type.</typeparam>
        /// <param name="frame">Bytes of Modbus frame.</param>
        /// <returns>New Modbus message based on type and frame bytes.</returns>
        public static T CreateModbusMessage<T> (byte[] frame)
        where T : IMewMsg, new ()
        {
            IMewMsg message = new T();
            message.Initialize (frame);
            return (T) message;
        }
        
        
    }
}
