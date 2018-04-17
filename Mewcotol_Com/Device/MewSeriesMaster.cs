using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using Mewcotol_Com.IO;

namespace Mewcotol_Com.Device
{
    public class MewSeriesMaster: MewMaster
    {
    
        public static MewSeriesMaster CreateSeriesMaster (string port, int baudRate, Parity parity)
        {
            return CreateSeriesMaster (new SerialPort (port, baudRate, parity));
        }
        
        
        public static MewSeriesMaster CreateSeriesMaster (SerialPort serialPort)
        {
            if (serialPort != null && !serialPort.IsOpen)
            {
                serialPort.Open();
            }
            
            return new MewSeriesMaster (new MewSeriesTransport (new SeriesStream (serialPort)));
        }
        
        public MewSeriesMaster (MewTransport trs) : base (trs)
        {
        }
        
    }
}
