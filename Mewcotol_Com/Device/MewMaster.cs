using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mewcotol_Com.IO;
using Mewcotol_Com.Message;

namespace Mewcotol_Com.Device
{
    public abstract class MewMaster : MewDevice, IMewMaster
    {
        public MewMaster (MewTransport trs) : base (trs)
        {
        }
        
        public bool ReadSingleCoil (byte stationid, string contactType, string addr)
        {
            var request = new RcsRequest (stationid);
            request.CreateRequestData (contactType, addr);
            return PerformReadDiscretes (request);
        }
        
        public void WriteSingleCoil (byte stationid, string contactType, string addr, byte open)
        {
            var request = new WcsRequest (stationid);
            request.CreateRequestData (contactType, addr, open);
            Transport.UnicastMessage<WriteSuccessResponse> (request);
        }
        
        public bool[] ReadMultiCoils (byte stationid, List<MutilCoils> coils)
        {
            var request = new RcpRequest (stationid);
            request.CreateRequestData (coils);
            return PerformReadDiscretesMulti (request, coils.Count);
        }
        
        public void WriteMultiCoils (byte stationid, List<MutilCoils> coils)
        {
            var request = new WcpRequest (stationid);
            request.CreateRequestData (coils);
            Transport.UnicastMessage<WriteSuccessResponse> (request);
        }
        
        public bool[] ReadMultiCoilsByWord (byte stationid, string type, int startAddr, int endAddr)
        {
            var request = new RccRequest (stationid);
            request.CreateRequestData (type, startAddr, endAddr);
            return PerformReadDiscretesMultiByWord (request, endAddr - startAddr);
        }
        
        public void WriteMultiCoilsByWord (byte stationid, string type, int startAddr, int endAddr, List<int> coils)
        {
            var request = new WccRequest (stationid);
            request.CreateRequestData (type, startAddr, endAddr, coils);
            Transport.UnicastMessage<WriteSuccessResponse> (request);
        }
        
        
        public void ResetCoils (byte stationid, string type, int startAddr, int endAddr, int value)
        {
            var request = new ScRequest (stationid);
            request.CreateRequestData (type, startAddr, endAddr, value);
            Transport.UnicastMessage<WriteSuccessResponse> (request);
        }
        
        public short[] ReadRegister (byte stationid, string type, int startAddr, int endAddr)
        {
            var request = new RdRequest (stationid);
            request.CreateRequestData (type, startAddr, endAddr);
            return PerformReadRegister (request, endAddr - startAddr);
        }
        
        public void WriteRegister (byte stationid, string type, int startAddr, int endAddr, List<int> regs)
        {
            var request = new WdRequest (stationid);
            request.CreateRequestData (type, startAddr, endAddr, regs);
            Transport.UnicastMessage<WriteSuccessResponse> (request);
        }
        
        public void ResetRegister (byte stationid, string type, int startAddr, int endAddr, int value)
        {
            var request = new SdRequest (stationid);
            request.CreateRequestData (type, startAddr, endAddr, value);
            Transport.UnicastMessage<WriteSuccessResponse> (request);
        }
        
        
        public short[] ReadTimerOrCounter (byte stationid, int startAddr, int endAddr)
        {
            string funcCode = "RS";
            return ReadTimer (funcCode, stationid, startAddr, endAddr);
        }
        
        public void WriteTimerOrCounter (byte stationid, int startAddr, int endAddr, List<int> regs)
        {
            string funcCode = "WS";
            WriteTimer (funcCode, stationid, startAddr, endAddr, regs);
        }
        
        public short[] ReadTimerOrCounterProcessValue (byte stationid, int startAddr, int endAddr)
        {
            string funcCode = "RK";
            return ReadTimer (funcCode, stationid, startAddr, endAddr);
        }
        
        public void WriteTimerOrCounterProcessValue (byte stationid, int startAddr, int endAddr, List<int> regs)
        {
            string funcCode = "WS";
            WriteTimer (funcCode, stationid, startAddr, endAddr, regs);
        }
        
        private short[] ReadTimer (string functioncode, byte stationid, int startAddr, int endAddr)
        {
            var request = new RskRequest (stationid, functioncode);
            request.CreateRequestData (startAddr, endAddr);
            return PerformReadTimer (request, endAddr - startAddr);
        }
        
        void WriteTimer (string functioncode, byte stationid, int startAddr, int endAddr, List<int> regs)
        {
            var request = new WskRequest (stationid, functioncode);
            request.CreateRequestData (startAddr, endAddr, regs);
            Transport.UnicastMessage<WriteSuccessResponse> (request);
        }
        
        
        public Task<bool> ReadSingleCoilAsync (byte stationid, string contactType, string addr)
        {
            return Task.Factory.StartNew (() => { return ReadSingleCoil (stationid, contactType, addr); });
        }
        
        public Task WriteSingleCoilAsync (byte stationid, string contactType, string addr, byte open)
        {
            return Task.Factory.StartNew (() => { WriteSingleCoil (stationid, contactType, addr, open); });
        }
        
        
        
        private bool PerformReadDiscretes (RcsRequest request)
        {
            ReadSuccessResponse response = Transport.UnicastMessage<ReadSuccessResponse> (request);
            return ! (response.Data == "0");
        }
        
        private bool[] PerformReadDiscretesMulti (RcpRequest request, int num)
        {
            ReadSuccessResponse response = Transport.UnicastMessage<ReadSuccessResponse> (request);
            var len = response.Data.Length;
            
            if (len != num)
            {
                throw new Exception ("read coils ,return coils num error");
            }
            
            string data = response.Data;
            bool[] results = new bool[len];
            
            for (var i = 0; i < num; i++)
            {
                if (string.Equals (data.Substring (i, 0), "1"))
                {
                    results[i] = true;
                }
                
                else
                {
                    results[i] = false;
                }
            }
            
            return results;
        }
        
        private bool[] PerformReadDiscretesMultiByWord (RccRequest request, int num)
        {
            ReadSuccessResponse response = Transport.UnicastMessage<ReadSuccessResponse> (request);
            var len = response.Data.Length;
            
            if (len != num)
            {
                throw new Exception ("read coils ,return coils num error");
            }
            
            string data = response.Data;
            short[] retData = new short[num];
            bool[] results = new bool[num * 16];
            retData = Utility.UtilityBase.HexToInt16 (data);
            
            for (var i = 0; i < num; i++)
            {
                for (var j = 0; j < 16; j++)
                {
                    results[i * 16 + j] = (retData[i] & (1 << j)) == 1;
                }
            }
            
            return results;
        }
        
        private short[] PerformReadRegister (RdRequest request, int num)
        {
            ReadSuccessResponse response = Transport.UnicastMessage<ReadSuccessResponse> (request);
            var len = response.Data.Length;
            
            if (len != num)
            {
                throw new Exception ("read coils ,return coils num error");
            }
            
            string data = response.Data;
            short[] results = new short[num];
            results = Utility.UtilityBase.HexToInt16 (data);
            return results;
        }
        
        private short[] PerformReadTimer (RskRequest request, int num)
        {
            ReadSuccessResponse response = Transport.UnicastMessage<ReadSuccessResponse> (request);
            var len = response.Data.Length;
            
            if (len != num)
            {
                throw new Exception ("read coils ,return coils num error");
            }
            
            string data = response.Data;
            short[] results = new short[num];
            results = Utility.UtilityBase.HexToInt16 (data);
            return results;
        }
        
        
        
    }
}