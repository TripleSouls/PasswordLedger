using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordLedger.Helpers
{
    class Security
    {

        private string key;
        private const int ivLength = 16;

        public Security() { }

        public Security(string key)
        {
            this.SetKey(key);
        }

        public void SetKey(string gkey)
        {
            this.key = gkey;
            if(this.key.Length < ivLength)
            {
                StringBuilder stringBuilder = new StringBuilder(key);
                int paddingLength = ivLength - this.key.Length;
                stringBuilder.Append('1', paddingLength);
                this.key = stringBuilder.ToString();
            }
            else if(this.key.Length > ivLength) {
                this.key = this.key.Substring(0, ivLength);
            }
        }

        public string Encode(string val)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(val);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            Array.Resize(ref keyBytes, ivLength);
            byte[] iv = new byte[ivLength];
            AesManaged aes = new AesManaged();

            aes.Key = keyBytes;
            aes.IV = iv;

            ICryptoTransform encrypter = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] encoded = encrypter.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
            return Convert.ToBase64String(encoded);
        }

        public string Decode(string val)
        {
            byte[] encoded = Convert.FromBase64String(val);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            Array.Resize(ref keyBytes, ivLength);
            byte[] iv = new byte[ivLength];
            AesManaged aes = new AesManaged();

            aes.Key = keyBytes;
            aes.IV = iv;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            byte[] decoded = decryptor.TransformFinalBlock(encoded, 0, encoded.Length);
            return Encoding.UTF8.GetString(decoded);
        }

        public static string CreateKey()
        {
            string val = Environment.ProcessId.ToString() + Environment.OSVersion.ToString() + Environment.UserName + DateTime.Now;
            val = GetHMAC(val);
            return val;
        }

        public static string GetHMAC(string val, string keys = "") {
            keys = (keys == "") ? "829471290432" : keys;
            byte[] key = Encoding.UTF8.GetBytes(keys);
            byte[] plainBytes = Encoding.UTF8.GetBytes(val);
            HMACSHA256 hmac = new HMACSHA256(key);
            byte[] hashedBytes = hmac.ComputeHash(plainBytes);
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
