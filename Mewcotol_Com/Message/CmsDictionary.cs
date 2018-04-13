using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mewcotol_Com.Message
{
    public static class CmsDictionary
    {
        private static readonly Dictionary<byte, string> _cmdMessages = CreateCmdMessages();
        
        
        private static Dictionary<byte, string> CreateCmdMessages()
        {
            Dictionary<byte, string> messages = new Dictionary<byte, string> (25);
            messages.Add (Mewcotol.FuncReadSingleCoilCode, Mewcotol.FuncReadSingleCoil);
            messages.Add (Mewcotol.FuncWriteSingleCoilCode, Mewcotol.FuncWriteSingleCoil);
            messages.Add (Mewcotol.FuncReadMultiCoilCode, Mewcotol.FuncReadMultiCoil);
            messages.Add (Mewcotol.FuncWriteMultiCoilCode, Mewcotol.FuncWriteMultiCoil);
            messages.Add (Mewcotol.FuncReadWordCoilCode, Mewcotol.FuncReadWordCoil);
            messages.Add (Mewcotol.FuncWriteWordCoilCode, Mewcotol.FuncWriteWordCoil);
            messages.Add (Mewcotol.PresetWordCoilCode, Mewcotol.PresetWordCoil);
            messages.Add (Mewcotol.ReadDataAreaCode, Mewcotol.ReadDataArea);
            messages.Add (Mewcotol.WriteDataAreaCode, Mewcotol.WriteDataArea);
            messages.Add (Mewcotol.PresetDataAreaCode, Mewcotol.PresetDataArea);
            messages.Add (Mewcotol.ReadTimerAreaCode, Mewcotol.ReadTimerArea);
            messages.Add (Mewcotol.WriteTimerAreaCode, Mewcotol.WriteTimerArea);
            return messages;
        }
        
        public static string GetCmdByCode (byte cmdid)
        {
            if (cmdid > 25)
            {
                throw new ArgumentOutOfRangeException ("命令代码超过25!");
            }
            
            return _cmdMessages[cmdid];
        }
        
        public static byte GetCmdCodeByStr (string cmd)
        {
            foreach (KeyValuePair<byte, string> kvp in _cmdMessages)
            {
                if (kvp.Value.Equals (cmd))
                {
                    return kvp.Key;
                }
            }
            
            return 0;
        }
        
        
    }
}
