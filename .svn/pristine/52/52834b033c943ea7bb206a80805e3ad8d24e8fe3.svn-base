using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace OliveFramework.tool
{
    public class Hash
    {
        public static string MD5(string str)
        {
            if (str == null)
                return "";

            byte[] data = _StringToBytes("GB2312", str);

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] md5Bytes = md5.ComputeHash(data);

            return _BytesToString(md5Bytes);
        }

        private static byte[] _StringToBytes(string name,string str)
        {
            return Encoding.GetEncoding(name).GetBytes(str);
        }

        private static string _BytesToString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }

            return sb.ToString().ToUpper();
        }
    }
}