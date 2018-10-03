using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Sans.CreditUnion.API.Tests.Shared
{
    public class HashWrapper
    {
        public static string GetChecksum(string file)
        {
            var checksum = string.Empty;

            string location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = Path.Combine(location, file);
            
            if(File.Exists(path))
            {
                using(var sha1 = SHA1.Create())
                {
                    using(var stream = File.OpenRead(path))
                    {
                        checksum = bytesToHexString(sha1.ComputeHash(stream));
                    }
                }
            }

            return checksum;
        }

        private static string bytesToHexString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length * 2);
            foreach(byte b in bytes)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }
    }
}