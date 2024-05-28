using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService2.Logic
{
    public class RSAHelper
    {
        public static RSAParameters ReadOpenSshPrivateKey(string privateKeyFile)
        {
            using var reader = new StringReader(File.ReadAllText(privateKeyFile));
            var pemReader = new PemReader(reader);
            var keyPair = (AsymmetricCipherKeyPair)pemReader.ReadObject();
            var rsaParameters = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)keyPair.Private);
            return rsaParameters;
        }

        public static byte[] GenerateSha256WithRsaSignature(byte[] data, RSAParameters rsaParameters)
        {
            using var rsa = RSA.Create();
            rsa.ImportParameters(rsaParameters);
            var hashAlgorithm = SHA256.Create();
            var hash = hashAlgorithm.ComputeHash(data);
            return rsa.SignHash(hash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }

        public static string GenerateSHA256withRSA(string data, RSAParameters privateKey)
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
            return Hex.ToHexString(resultData);

            // return Convert.ToBase64String(resultData);
        }
    }
}
