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
            // configure serial port
            int baudRate = 9600;
            string portid = "COM1";
            Parity pt = Parity.None;
            
            using (var port = new SerialPort (portid, baudRate, pt))
            {
                // create modbus master
                IMewMaster master = MewSeriesMaster.CreateSeriesMaster (port) ;
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
