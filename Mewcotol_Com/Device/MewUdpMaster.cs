using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mewcotol_Com.Device
{

    using Mewcotol_Com.IO;
    using System.Net;
    using System.Net.Sockets;
    
    public class MewUdpMaster : MewMaster
    {
    
        public static MewUdpMaster CreateTcpMaster (IPEndPoint localEP)
        {
            return CreateSeriesMaster (new UdpClient (localEP));
        }
        
        
        public static MewUdpMaster CreateSeriesMaster (UdpClient udpClient)
        {
            if (udpClient != null)
            {
            }
            
            return new MewUdpMaster (new MewTransport (new UdpClientAdapter (udpClient)));
        }
        
        public MewUdpMaster (MewTransport trs) : base (trs)
        {
        }
        
        
    }
}
