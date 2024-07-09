using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace capaModelo.Modelos
{
    public class EncryptUsuarios
    {

        public string _password { get; set; }
        public int _byteKey { get; set; }


        /// <summary>
        /// Encrypta una cadena de caracteres a HASH
        /// </summary>
        /// <param name="encrypt"></param>
        /// <returns></returns>
        public string Encrypt(EncryptUsuarios encrypt)
        {
            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encrypt._password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                byte[] key = pdb.GetBytes(encrypt._byteKey);
                byte[] IV = pdb.GetBytes(encrypt._byteKey);

                ICryptoTransform encryptor = aes.CreateEncryptor(key, IV);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(encrypt._password);
                        }

                        encrypt._password = Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }

            return encrypt._password;
        }

        /// <summary>
        /// Descrypta el HASH a cadena de caracteres (Ingenieria Inversa)
        /// </summary>
        /// <param name="decrypt"></param>
        /// <returns></returns>
        public string Decrypt(EncryptUsuarios decrypt)
        {
            string claveDecriptada = string.Empty;
            byte[] cipherBytes = Convert.FromBase64String(decrypt._password);

            using (Aes Decryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(decrypt._password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                byte[] key = pdb.GetBytes(decrypt._byteKey);
                byte[] IV = pdb.GetBytes(decrypt._byteKey);
                Decryptor.Padding = PaddingMode.None;

                ICryptoTransform Descypt = Decryptor.CreateDecryptor(key, IV);

                using (MemoryStream memoryStream = new MemoryStream(cipherBytes))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, Descypt, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            claveDecriptada = streamReader.ReadToEnd();
                        }

                    }

                }
            }
            return claveDecriptada;
        }



    }
}
