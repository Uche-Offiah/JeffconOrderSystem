using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace FHIRDataExchange
{
    public static class Utility
    {
        public static void LogStuff(string stuff, string folderName)
        {
            var directory = $"C:/{folderName}/";
            var fileName = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.DayOfYear.ToString() + "_" + "Log.txt";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.AppendAllText(directory + fileName, Environment.NewLine + DateTime.Now.ToString() + Environment.NewLine + stuff + Environment.NewLine + Environment.NewLine);
        }

        private static readonly byte[] Key = Encoding.UTF8.GetBytes("j36KOulTmsD25efkGiVNRztxMfmFjtll"); // 32 bytes key
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("fEDryJoy78APKUIy"); // 16 bytes IV

        // Encrypt the password
        public static string Encrypt(string plainText)
        {
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Key;
                    aes.IV = IV;

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter sw = new StreamWriter(cs))
                            {
                                sw.Write(plainText);
                            }
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception err)
            {
                throw;
            }
        }

        // Decrypt the password
        public static string Decrypt(string cipherText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

        public static ByteArrayContent Base64ToFIle(string base64File)
        {
            var fileBytes = Convert.FromBase64String(base64File);
            var fileContent = new ByteArrayContent(fileBytes);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
            return fileContent;
        }

        public static ByteArrayContent Base64ToFIle(byte[] fileBytes)
        {
            var fileContent = new ByteArrayContent(fileBytes);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
            return fileContent;
        }

        // Validates a Nigerian phone number.
        // Accepts formats like 08012345678 or +2348012345678.
        public static bool IsValidNigerianPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            // Remove spaces, dashes, and parentheses
            phoneNumber = Regex.Replace(phoneNumber, @"[\s\-\(\)]", "");

            var pattern = @"^(0[789]\d{8}|\+234[789]\d{8})$";

            return Regex.IsMatch(phoneNumber, pattern);
        }


        private static readonly Random _random = new Random();


        public static string GeneratePhoneNumber(bool internationalFormat = false)
        {
            // Valid Nigerian GSM prefixes (common carriers)
            string[] prefixes = { "70", "80", "81", "90", "91" };

            // Pick a random prefix
            string prefix = prefixes[_random.Next(prefixes.Length)];

            // Generate remaining 8 digits
            string numberBody = _random.Next(10000000, 99999999).ToString();

            if (internationalFormat)
            {
                return $"+234{prefix}{numberBody}";
            }
            else
            {
                return $"0{prefix}{numberBody}";
            }
        }

    }
}
