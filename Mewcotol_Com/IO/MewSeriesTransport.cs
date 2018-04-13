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
        internal override void OnValidateResponse (IMewMsg request, IMewMsg response)
        {
            if (request.StationID != response.StationID)
            {
                string msg = $"Response was not of expected transaction ID. Expected {request.StationID}, received {response.StationID}.";
                throw new Exception (msg);
            }
        }
        
        
        internal override IMewMsg ReadResponse<T>()
        {
            var response = StreamUtility.ReadLine (StreamResource);
            
            if (response.Length == 0)
            {
                return new T();
            }
            
            byte[] frame = Encoding.ASCII.GetBytes (response);
            return  base.CreateResponse<T> (frame);
        }
        
        
        
        internal override void Write (IMewMsg message)
        {
            byte[] frame = message.BuildMessageFrame();
            Debug.WriteLine ($"TX: {string.Join(", ", frame)}");
            StreamResource.Write (frame, 0, frame.Length);
        }
    }
}
