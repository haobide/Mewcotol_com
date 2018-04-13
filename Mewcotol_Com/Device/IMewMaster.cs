using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mewcotol_Com.IO;
using Mewcotol_Com;


namespace Mewcotol_Com.Device
{
    public interface IMewMaster : IDisposable
    {
        MewTransport Transport { get; }
        
        bool ReadSingleCoil (byte stationid, string contactType, string  addr);
        
        void WriteSingleCoil (byte stationid, string contactType, string addr, byte bON);
        
        bool[] ReadMultiCoils (byte stationid, List<MutilCoils> coils);
        
        void WriteMultiCoils (byte stationid, List<MutilCoils> coils);
        
        bool[] ReadMultiCoilsByWord (byte stationid, string type, int startAddr, int endAddr);
        
        void WriteMultiCoilsByWord (byte stationid, string type, int startAddr, int endAddr, List<int> coils);
        
        void ResetCoils (byte stationid, string type, int startAddr, int endAddr, int value);
        
        short[] ReadRegister (byte stationid, string type, int startAddr, int endAddr);
        
        void WriteRegister (byte stationid, string type, int startAddr, int endAddr, List<int> regs);
        
        void ResetRegister (byte stationid, string type, int startAddr, int endAddr, int value);
        
        short[] ReadTimerOrCounter (byte stationid, int startAddr, int endAddr);
        
        void WriteTimerOrCounter (byte stationid, int startAddr, int endAddr, List<int> regs);
        
        short[] ReadTimerOrCounterProcessValue (byte stationid, int startAddr, int endAddr);
        
        void WriteTimerOrCounterProcessValue (byte stationid, int startAddr, int endAddr, List<int> regs);
        
    }
}
