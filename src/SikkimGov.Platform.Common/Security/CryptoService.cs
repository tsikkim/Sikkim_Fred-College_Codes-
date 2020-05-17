using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using SikkimGov.Platform.Common.Security.Contracts;

namespace SikkimGov.Platform.Common.Security
{
    public class CryptoService : ICryptoService, IDisposable
    {
        private TripleDESCryptoServiceProvider mobjDES;

        public CryptoService()
        {
            Construct();
        }

        /// <summary>
        ///     Create a secret key. The key is used to encrypt and decrypt strings.
        ///     Without the key, the encrypted string cannot be decrypted and is just
        ///     garbage.
        ///     You must use the same key to decrypt an encrypted string as the string
        ///     was originally encrypted with.
        /// </summary>
        private void Construct()
        {
            var strKey = ConfigurationManager.AppSettings["passwordEncryptionKey"];
            byte[] abytKeyHash;

            /// <remarks>
            ///     Generate an MD5 hash from the key.
            ///     A hash is a one way encryption meaning once you generate
            ///     the hash, you can't derive the key back from it.
            /// </remarks>
            var hashmd5 = new MD5CryptoServiceProvider();
            abytKeyHash = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(strKey));

            mobjDES = new TripleDESCryptoServiceProvider();
            // implement DES3 encryption
            mobjDES.Key = abytKeyHash;
            // the key is the secret key hash.
            /// <remarks>
            ///     The mode is the block cipher mode which is basically the details of how the encryption will work.
            ///     There are several kinds of ciphers available in DES3 and they all have benefits and drawbacks.
            ///     Here the Electronic Codebook cipher is used which means that a given bit of text is always encrypted
            ///     exactly the same when the same key is used.
            ///     </remarks>
            // CBC, CFB
            mobjDES.Mode = CipherMode.ECB;
        }

        /// <summary>
        ///     To encrypt an unencrypted string
        /// </summary>
        /// <param name="input">Text to be encrypted</param>
        /// <returns>Encrypted text</returns>
        public string Encrypt(string input)
        {
            try
            {
                var mabtyBuffer = Encoding.UTF8.GetBytes(input);
                return Convert.ToBase64String(mobjDES.CreateEncryptor().TransformFinalBlock(mabtyBuffer, 0, mabtyBuffer.Length));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///     To decrypt an encrypted string
        /// </summary>
        /// <param name="input">Text to be decrypted</param>
        /// <returns>Decrypted text</returns>
        public string Decrypt(string input)
        {
            try
            {
                var mabtyBuffer = Convert.FromBase64String(input);
                return Encoding.UTF8.GetString(mobjDES.CreateDecryptor().TransformFinalBlock(mabtyBuffer, 0, mabtyBuffer.Length));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ConvertStringtoMD5(string strword)
        {
            MD5 md5__1 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(strword);
            byte[] hash = md5__1.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= hash.Length - 1; i++)
                sb.Append(hash[i].ToString("x2"));
            return sb.ToString();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (mobjDES != null)
                    mobjDES = null;
            }
        }

        ~CryptoService()
        {
            Dispose(false);
        }
    }
}
