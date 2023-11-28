using Microsoft.CodeAnalysis.Operations;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using Testing.DanamonNew.Utility;
using static Testing.DanamonNew.Utility.SD;
using Testing.DanamonNew.Models.Dto;
using System.Security.Cryptography;
using Microsoft.CodeAnalysis.Text;


namespace Testing.DanamonNew.Logic
{
    public class GeneralLogic
    {

        public string GenerateSigniture(SD.ApiType apiType, SD.FunctionDBIType functionDBIType, string contentText)
        {
            string result = string.Empty;
            string rawData = string.Empty;
            RSAParameters privateKey = new RSAParameters();
            string textFilePath = @"";




            switch (functionDBIType)
            {
                case FunctionDBIType.SC_73:
                    if (File.Exists(textFilePath))
                    {
                        string[] strKey = File.ReadAllLines(textFilePath);
                    }

                    using (var stream = File.OpenRead("@tmp"))
                    {
                        using (var reader = new PemUtils.PemReader(stream))
                        {
                            privateKey = reader.ReadRsaKey();
                        }
                    }


                    break;
                case FunctionDBIType.SC_24:
                case FunctionDBIType.SC_25:


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
                    break;
            }

            result = GenerateSHA256(rawData);

            return result;
        }

        private string GenerateSHA256(string contentText)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(contentText);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


        private string GenerateSHA256withRSA(string contentText, RSAParameters privateKey)
        {
            byte[] resultData; string result = string.Empty;
            using (var rsa = RSA.Create())
            {
                rsa.ImportParameters(privateKey);

                byte[] dataBytes = Encoding.UTF8.GetBytes(contentText);
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashedData = sha256.ComputeHash(dataBytes);
                    resultData = rsa.SignHash(hashedData, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                }
            }

            return Convert.ToBase64String(resultData);
        }


        private string GenerateHMAC_SHA512(string contentText, string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] messageBytes = Encoding.UTF8.GetBytes(contentText);

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
