using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlaylister_Server
{
    class Hashing
    {

        private static RNGCryptoServiceProvider salt = null;
        private const int SALT_SIZE = 24;

        //generate salt as string
        public string GetSaltString()
        {
            salt = new RNGCryptoServiceProvider();

            byte[] saltBytes = new byte[SALT_SIZE];

            salt.GetNonZeroBytes(saltBytes);

            string saltString = Utility.GetString(saltBytes);

            return saltString;
        }

        public string SaltedHashGen(string password, string salt)
        {

            string beforeSha = password + salt;

            var crypt = new SHA256Managed();
            var hash = new StringBuilder();//convert password + salt into bytes
            byte[] resultBytes = crypt.ComputeHash(Encoding.UTF8.GetBytes(beforeSha));
            //salt comes out unique everytime, will have to append to user variables and compare name and get salt and add that to password validation attempts
            return Utility.GetString(resultBytes);
        }

        internal class Utility
        {
            public static byte[] GetBytes(string str)
            {
                byte[] bytes = new byte[str.Length * sizeof(char)];
                System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
                return bytes;
            }

            public static string GetString(byte[] bytes)
            {
                char[] chars = new char[bytes.Length / sizeof(char)];
                System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
                return new string(chars);
            }
        }

    }

}

