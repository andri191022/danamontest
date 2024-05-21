using System.Text;
using Testing.DanamonNew.Utility;
using static Testing.DanamonNew.Utility.SD;
using System.Security.Cryptography;

namespace Testing.DanamonNew.Logic
{
    public class GeneralLogic
    {

        public static string GenerateSigniture(SD.ApiType apiType, SD.FunctionDBIType functionDBIType, string data)
        {
            try
            {
                string result = string.Empty;
               // string rawData = string.Empty;
                RSAParameters privateKey = new RSAParameters();
                string textFilePath = Path.Combine(Environment.CurrentDirectory, @"Data\", "id_rsa");

                //string fileContent = string.Empty;
                //var rsaKeyGenerator = new RsaKeyGenerator();
                // var privkey = rsaKeyGenerator.GenerateKeyPair(2048);

                switch (functionDBIType)
                {
                    case FunctionDBIType.SC_73:
                        if (File.Exists(textFilePath))
                        {
                            privateKey = RSAHelper.ReadOpenSshPrivateKey(textFilePath);
                            result = RSAHelper.GenerateSHA256withRSA(data, privateKey);
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

                //result = GenerateSHA256(rawData);

                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //private static string GenerateSHA256(string contentText)
        //{
        //    using (SHA256 sha256 = SHA256.Create())
        //    {
        //        byte[] inputBytes = Encoding.UTF8.GetBytes(contentText);
        //        byte[] hashBytes = sha256.ComputeHash(inputBytes);

        //        StringBuilder builder = new StringBuilder();
        //        for (int i = 0; i < hashBytes.Length; i++)
        //        {
        //            builder.Append(hashBytes[i].ToString("x2"));
        //        }
        //        return builder.ToString();
        //    }
        //}


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

        //private static string GenerateSHA256withRSA(string data, string privateKey)
        //{
        //    // Create a new instance of the RSACryptoServiceProvider class
        //    using (var rsa = new RSACryptoServiceProvider())
        //    {
        //        // Import the private key
        //        rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);

        //        // Create a new instance of the SHA256 class
        //        using (var sha256 = SHA256.Create())
        //        {
        //            // Compute the hash of the data
        //            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));

        //            // Sign the hash with the private key
        //            var signature = rsa.SignHash(hash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

        //            // Return the signature as a base64-encoded string
        //            return Convert.ToBase64String(signature);
        //        }
        //    }
        //}


        //private static string GGG()
        //{
        //    string data = "Hello, world!";
        //    string privateKey = "-----BEGIN RSA PRIVATE KEY-----\nMIIEowIBAAKCAQEA3GkdqJEUFUcTkZVHdF2aQn/mBMlPcZiIwTUwCYXaYMKF2OEpJPZ/s+lzrCsIJU0WuGwNiZdBGqQrFkAqwEb6dVNybTmr/NQWtGKcRWvHgWWxCvfTpDJOa/IhkfqbCQeWtEgwKG1dvJEyYUrCJmVVdLqRDnjCvvIWcZHhOaCwLnfDnMX+rOEp+EZOJtq2YmRuuPcKbYIrAqzIiM0qOcDnTcAw0WmGwNybJkf/TQdEuHXbTvHkWrMG+sGmrOE5yB0OzDnIiJPnWmwvNi5TsE/cD0s1i/mWmIjNnHdFkA6Dd1GcqUgLdvIbx2sEuC6nkFJbLs1qvNKC3mr/pTd7LKcXgA2O2qxCZfTNXOdSyJGg8W0C7WWt8uRKGhLvJhDsRNsEuQcFsFWwmOaIkNiCJcE/yFZTcSy4b8dvhgC3oqQ7cjnFmwmJ8wkHKb38oBZuHrjn1SdKWdSvHcZvUc/uTdEeKcMmLHQhxuMNsPmXoEpPJQTaWbBVePkWu/IxkdvP2aFvUYaC/HnFQMVn/aBnI0HvRiOXWwLnkJFuVsTvPv+PuOQaCiIUeBvPcBMdIa+qA==\n-----END RSA PRIVATE KEY-----";

        //    var rsa = RSA.Create();
        //    rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out int _);

        //    var dataBytes = Encoding.UTF8.GetBytes(data);
        //    var signatureBytes = rsa.SignData(dataBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

        //    return Convert.ToBase64String(signatureBytes);


        //    //string signature = RSAKeySignatureGenerator.GenerateSignature(data, privateKey);

        //    //Console.WriteLine($"Signature: {signature}");
        //}
    }
}