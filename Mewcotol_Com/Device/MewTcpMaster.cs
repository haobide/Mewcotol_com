using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;


namespace Mewcotol_Com.Device
{
    using Mewcotol_Com.IO;
    using System.Net;
    
    public class MewTcpMaster : MewMaster
    {
    
        public static MewTcpMaster CreateTcpMaster (IPEndPoint localEP)
        {
            return CreateSeriesMaster (new TcpClient (localEP));
        }
        
        
        public static MewTcpMaster CreateSeriesMaster (TcpClient tcpClient)
        {
            if (tcpClient != null)
            {
            }
            
            return new MewTcpMaster (new MewTransport (new TcpClientAdapter (tcpClient)));
        }
        
        public MewTcpMaster (MewTransport trs) : base (trs)
        {
        }
        public override void Dispose()
        {
            Dispose (true);
            GC.SuppressFinalize (this);
        }
        
    }
}
