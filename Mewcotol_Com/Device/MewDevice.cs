using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mewcotol_Com.IO;
using Mewcotol_Com.Utility;

namespace Mewcotol_Com.Device
{
    public abstract class MewDevice: IDisposable
    {
        private MewTransport _mewTransport;
        
        internal MewDevice (MewTransport mewtrs)
        {
            _mewTransport = mewtrs;
        }
        
        
        public MewTransport Transport
        {
            get { return _mewTransport; }
        }
        
        protected void Dispose (bool disposing)
        {
            if (disposing)
            {
                DisposableUtility.Dispose (ref _mewTransport);
            }
        }
        
        public virtual void Dispose()
        {
            if (_mewTransport != null)
            {
                Dispose (true);
            }
            
            GC.SuppressFinalize (this);
        }
    }
}
