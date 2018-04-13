using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mewcotol_Com;
using Mewcotol_Com.Device;
using Mewcotol_Com.IO;
using System.IO.Ports;

namespace Samples
{
    class Program
    {
        static void Main (string[] args)
        {
            try
            {
                MewSerialMasterReadCoils();
            }
            
            catch (Exception e)
            {
                Console.WriteLine (e.Message);
            }
            
            Console.ReadKey();
        }
        
        public static void MewSerialMasterReadCoils()
        {
            using (SerialPort port = new SerialPort ("COM1"))
            {
                // configure serial port
                port.BaudRate = 9600;
                port.DataBits = 8;
                port.Parity = Parity.None;
                port.StopBits = StopBits.One;
                port.Open();
                
                using (var stream = new SeriesStream (port))
                {
                    var adapter = new MewSeriesTransport (stream);
                    // create modbus master
                    IMewMaster master = new MewMaster (adapter);
                    byte stationId = 1;
                    string coilType = "Y";
                    string startAddress = "013A";
                    // read five registers
                    bool open = master.ReadSingleCoil (stationId, coilType, startAddress);
                    Console.WriteLine ($"coils{coilType} {startAddress}={open}");
                }
            }
        }
        
        
    }
}
