using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mewcotol_Com.Utility
{
    public static class UtilityBase
    {
        public static byte[] StringToByte (string src)
        {
            if (src == null)
            {
                throw new ArgumentNullException (nameof (src));
            }
            
            byte[] byteArray = System.Text.Encoding.ASCII.GetBytes (src);
            return byteArray;
        }
        
        private static byte Bcc (params byte[] src)
        {
            byte results = 0;
            
            for (var i = 0; i < src.Length; i++)
            {
                results ^= src[i];
            }
            
            return results;
        }
        
        
        public static string ByteToHexStr (byte src)
        {
            return Convert.ToString (src, 16).ToUpper();
        }
        
        public static string GetBccStr (string src)
        {
            return ByteToHexStr (GetBccByte (src));
        }
        
        
        public static byte GetBccByte (string src)
        {
            return Bcc (StringToByte (src));
        }
        
        
        public static byte[] HexToBytes (string hex)
        {
            if (hex == null)
            {
                throw new ArgumentNullException (nameof (hex));
            }
            
            if (hex.Length % 2 != 0)
            {
                throw new FormatException ("hex数据长度不是偶数!");
            }
            
            byte[] results = new byte[hex.Length / 2];
            
            for (var i = 0; i < results.Length; i++)
            {
                results[i] = Convert.ToByte (hex.Substring (i * 2, 2), 16);
            }
            
            return results;
        }
        
        public static short[] ByteToInt16 (params byte[] src)
        {
            if (src.Length < 0)
            {
                throw new ArgumentNullException (nameof (src));
            }
            
            if (src.Length % 2 != 0)
            {
                throw new FormatException ("hex数据长度不是偶数!");
            }
            
            short[] results = new short[src.Length / 2];
            
            for (var i = 0; i < results.Length; i++)
            {
                results[i] = (short) BitConverter.ToInt16 (src, i * 2);
            }
            
            return results;
        }
        
        public static short ByteToShort (params byte[]src)
        {
            return (short) BitConverter.ToInt16 (src, 0);
        }
        
        public static short DecimalToShort (string src, int startindex, int count)
        {
            return Convert.ToInt16 (src.Substring (startindex, count));
        }
        
        public static string ShortToString (short src)
        {
            return Convert.ToString (src);
        }
        //将数据从3412顺序 16进制编码转换成short数组
        public static short[] HexToInt16 (string hex)
        {
            return ByteToInt16 (HexToBytes (hex));
        }
        
        
        //static void Main (string[] args)
        //{
        //    string str = "%01#RCSX0000";
        //    string strHex = "63004433";
        //    short testNum = 99;
        //    byte test = 3;
        //    byte[] array = new byte[2] { 49, 67 };
        //    string begin = Encoding.ASCII.GetString (array);
        //    byte rs = Convert.ToByte (begin, 16);
        //    const char fail = '!';
        //    Console.WriteLine ($"{GetBccStr(str)},,,{HexToInt16(strHex).Last()}...{ShortToString(testNum)}...{test:D2}...{(byte)fail}");
        //    Console.WriteLine ($"{rs}");
        //    Console.ReadLine();
        //}
    }
    
}
