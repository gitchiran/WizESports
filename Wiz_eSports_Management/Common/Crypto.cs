using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Wiz_eSports_Management.Common
{
    public static class Crypto
    {
        private static byte[] _keyByte = { };
        //Default Key
        private static string _key = "Pass@123#";
        //Default initial vector
        private static byte[] _ivByte = { 0x01, 0x12, 0x23, 0x34, 0x45, 0x56, 0x67, 0x78 };

        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "abc123";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                    }

                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }

            return clearText;
        }

        public static string Decrypt(string cipherText)
        {
            var EncryptionKey = "abc123";
            cipherText = cipherText.Replace(" ", "+");
            var cipherBytes = Convert.FromBase64String(cipherText);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return cipherText;
        }

        public static string ComputeHash(string s)
        {
            var enc = Encoding.GetEncoding(0);
            byte[] buffer = enc.GetBytes(s);
            var sha1 = SHA256.Create();
            var hashedString = BitConverter.ToString(sha1.ComputeHash(buffer)).Replace("-", "");

            return hashedString;
        }

        public static String sha256_hash(String value)
        {
            using (SHA256 hash = SHA256Managed.Create())
            {
                return Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(value.ToCharArray())));
            }
        }

        public static string base64Decode(string sData) //Decode    
        {
            try
            {
                var encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecodeByte = Convert.FromBase64String(sData);
                int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                char[] decodedChar = new char[charCount];
                utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
                string result = new String(decodedChar);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Decode" + ex.Message);
            }
        }

        public static string Decrypt(string value, string key, string iv)
        {

            string decrptValue = string.Empty;
            if (!string.IsNullOrEmpty(value))
            {
                MemoryStream ms = null;
                CryptoStream cs = null;
                value = value.Replace(" ", "+");
                byte[] inputByteArray = new byte[value.Length];
                try
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        _keyByte = Encoding.UTF8.GetBytes
                                (key.Substring(0, 8));
                        if (!string.IsNullOrEmpty(iv))
                        {
                            _ivByte = Encoding.UTF8.GetBytes
                                (iv.Substring(0, 8));
                        }
                    }
                    else
                    {
                        _keyByte = Encoding.UTF8.GetBytes(_key);
                    }
                    using (DESCryptoServiceProvider des =
                            new DESCryptoServiceProvider())
                    {
                        inputByteArray = Convert.FromBase64String(value);
                        ms = new MemoryStream();
                        cs = new CryptoStream(ms, des.CreateDecryptor
                        (_keyByte, _ivByte), CryptoStreamMode.Write);
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        Encoding encoding = Encoding.UTF8;
                        decrptValue = encoding.GetString(ms.ToArray());
                    }
                }
                catch
                {
                    //TODO: write log 
                }
                finally
                {
                    cs.Dispose();
                    ms.Dispose();
                }
            }
            return decrptValue;
        }

        ////////public static string Decrypt(string encryptedText, string password)
        ////////{
        ////////    if (encryptedText == null)
        ////////    {
        ////////        return null;
        ////////    }

        ////////    if (password == null)
        ////////    {
        ////////        password = String.Empty;
        ////////    }

        ////////    // Get the bytes of the string
        ////////    var bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
        ////////    var passwordBytes = Encoding.UTF8.GetBytes(password);

        ////////    passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

        ////////    var bytesDecrypted = Decrypt(bytesToBeDecrypted, passwordBytes);

        ////////    return Encoding.UTF8.GetString(bytesDecrypted);
        ////////}

        ////////private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        ////////{
        ////////    byte[] decryptedBytes = null;

        ////////    // Set your salt here, change it to meet your flavor:
        ////////    // The salt bytes must be at least 8 bytes.
        ////////    var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        ////////    using (MemoryStream ms = new MemoryStream())
        ////////    {
        ////////        using (RijndaelManaged AES = new RijndaelManaged())
        ////////        {
        ////////            var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

        ////////            AES.KeySize = 256;
        ////////            AES.BlockSize = 128;
        ////////            AES.Key = key.GetBytes(AES.KeySize / 8);
        ////////            AES.IV = key.GetBytes(AES.BlockSize / 8);
        ////////            AES.Mode = CipherMode.CBC;

        ////////            using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
        ////////            {
        ////////                cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
        ////////                cs.Close();
        ////////            }

        ////////            decryptedBytes = ms.ToArray();
        ////////        }
        ////////    }

        ////////    return decryptedBytes;
        ////////}

        ////////public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        ////////{
        ////////    byte[] decryptedBytes = null;

        ////////    // Set your salt here, change it to meet your flavor:
        ////////    // The salt bytes must be at least 8 bytes.
        ////////    byte[] saltBytes = new byte[] { 2, 1, 7, 3, 6, 4, 8, 5 };

        ////////    using (MemoryStream ms = new MemoryStream())
        ////////    {
        ////////        using (RijndaelManaged AES = new RijndaelManaged())
        ////////        {
        ////////            AES.KeySize = 256;
        ////////            AES.BlockSize = 128;

        ////////            var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
        ////////            AES.Key = key.GetBytes(AES.KeySize / 8);
        ////////            AES.IV = key.GetBytes(AES.BlockSize / 8);

        ////////            AES.Mode = CipherMode.CBC;

        ////////            using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
        ////////            {
        ////////                cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
        ////////                cs.Close();
        ////////            }
        ////////            decryptedBytes = ms.ToArray();
        ////////        }
        ////////    }

        ////////    return decryptedBytes;
        ////////}
    }
}
