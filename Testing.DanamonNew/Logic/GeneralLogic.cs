using Microsoft.CodeAnalysis.Operations;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using Testing.DanamonNew.Utility;
using static Testing.DanamonNew.Utility.SD;
using Testing.DanamonNew.Models.Dto;
using System.Security.Cryptography;


namespace Testing.DanamonNew.Logic
{
    public class GeneralLogic
    {

        public string GenerateSigniture(SD.ApiType apiType)
        {
            string result = string.Empty;
            string rawData = string.Empty;

            switch (apiType)
            {
                case ApiType.POST:
                case ApiType.PUT:
                    //relative url +bdi timestamp + secret key + additional key + request body



                    break;
                case ApiType.DELETE:
                case ApiType.GET:
                    //relative url +bdi timestamp + secret key + additional key




                    break;
                default:
                    break;
            }

            result = GenerateSHA256(rawData);

            return result;
        }

        private string GenerateSHA256(string text)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2")); 
                }
                return builder.ToString();
            }
        }


        private string GenerateSHA256withRSA(string data, RSAParameters privateKey)
        {
            byte[] resultData; string result = string.Empty;
            using (var rsa = RSA.Create())
            {
                rsa.ImportParameters(privateKey);

                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashedData = sha256.ComputeHash(dataBytes);
                    resultData = rsa.SignHash(hashedData, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                }
            }

            return Convert.ToBase64String(resultData);
        }


        private string GenerateHMAC_SHA512(string message, string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

            using (HMACSHA512 hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashBytes = hmac.ComputeHash(messageBytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2")); // Convert each byte to its hexadecimal representation
                }
                return builder.ToString();
            }

        }
}
