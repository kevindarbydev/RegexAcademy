using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RegexAcademy
{
    public class PasswordEncryptor
    {

        public static string EncryptPassword(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(ASCIIEncoding.UTF8.GetBytes(password));

            byte[] result = md5.Hash;

            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString());
            }

            return sb.ToString();
        }
    }
}
