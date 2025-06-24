using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Mail.Engine.Service.Core.Helpers
{
    public static class EncryptionHelper
    {
        private static readonly Aes _aes = Aes.Create();

        static EncryptionHelper()
        {
            _aes.Key = Encoding.UTF8.GetBytes("ui8uiyjui89oiuy7");
        }
        public static string Encrypt(string plainText)
        {
            try
            {
                _aes.GenerateIV();

                var iv = _aes.IV;

                var encryptor = _aes.CreateEncryptor(_aes.Key, iv);
                using var memoryStream = new MemoryStream();
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using var streamWriter = new StreamWriter(cryptoStream);
                    streamWriter.Write(plainText);
                }

                var encryptedBytes = memoryStream.ToArray();
                var result = new byte[iv.Length + encryptedBytes.Length];
                Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                Buffer.BlockCopy(encryptedBytes, 0, result, iv.Length, encryptedBytes.Length);
                return HttpUtility.UrlEncode(Convert.ToBase64String(result));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Encryption failed: " + ex.Message);
                return string.Empty;
            }
        }
        public static string Decrypt(string cipherText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cipherText)) throw new ArgumentException("cipherText is null or empty.");

                cipherText = HttpUtility.UrlDecode(cipherText);
                cipherText = cipherText.Replace(' ', '+'); // Handle spaces if present

                // Fix potential padding issues if necessary
                switch (cipherText.Length % 4)
                {
                    case 2:
                        cipherText += "==";
                        break;
                    case 3:
                        cipherText += "=";
                        break;
                }

                var decodedBytes = Convert.FromBase64String(cipherText);

                // Ensure the decoded bytes are at least the length of IV + 1 byte
                if (decodedBytes.Length < _aes.BlockSize / 8) throw new OverflowException("Decoded bytes length is less than the IV length.");

                var iv = new byte[_aes.BlockSize / 8];
                var cipher = new byte[decodedBytes.Length - iv.Length];

                Buffer.BlockCopy(decodedBytes, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(decodedBytes, iv.Length, cipher, 0, cipher.Length);

                _aes.IV = iv;
                var decryptor = _aes.CreateDecryptor(_aes.Key, iv);

                using var memoryStream = new MemoryStream(cipher);
                using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                using var streamReader = new StreamReader(cryptoStream);

                return streamReader.ReadToEnd();
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Decryption failed - Invalid format: " + ex.Message);
                return string.Empty;
            }
            catch (OverflowException ex)
            {
                Console.WriteLine("Decryption failed - Overflow error: " + ex.Message);
                return string.Empty;
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine("Decryption failed - Cryptographic error: " + ex.Message);
                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Decryption failed - General error: " + ex.Message);
                return string.Empty;
            }
        }

    }
}
