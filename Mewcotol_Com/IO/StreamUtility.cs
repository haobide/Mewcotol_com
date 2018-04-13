using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mewcotol_Com.IO
{
    public static class StreamUtility
    {
    
        public static string  ReadLine (IStreamResource stream)
        {
            const string NewLine = "\r\n";
            StringBuilder results = new StringBuilder();
            byte[] singleByte = new byte[1];
            
            do
            {
                stream.Read (singleByte, 0, 1);
                results.Append (System.Text.Encoding.ASCII.GetChars (singleByte).First());
            }
            while (results.ToString().EndsWith (NewLine));
            
            return results.ToString().Substring (0, results.Length - NewLine.Length);
        }
        
        
    }
}
