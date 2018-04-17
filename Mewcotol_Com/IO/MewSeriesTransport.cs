using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mewcotol_Com.Message;
using System.Diagnostics;

namespace Mewcotol_Com.IO
{
    public class MewSeriesTransport: MewTransport
    {
        public MewSeriesTransport()
        {
        }
        
        public  MewSeriesTransport (IStreamResource streamResource) : base (streamResource)
        {
        }
        
        
        
        internal override IMewMsg ReadResponse<T>()
        {
            var response = StreamResource.ReadLine ();
            
            if (response.Length == 0)
            {
                return new T();
            }
            
            byte[] frame = Encoding.ASCII.GetBytes (response);
            return  base.CreateResponse<T> (frame);
        }
        
        
        
        
    }
}
