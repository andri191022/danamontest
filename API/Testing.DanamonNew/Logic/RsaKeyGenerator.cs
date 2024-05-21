using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Pqc.Crypto.Lms;


namespace Testing.DanamonNew.Logic
{
    public class RsaKeyGenerator
    {
        public string GenerateKeyPair(int keySize)
        {
            var random = new SecureRandom();
            var keyGenerationParameters = new KeyGenerationParameters(random, keySize);
            RsaKeyPairGenerator generator = new RsaKeyPairGenerator();
            generator.Init(keyGenerationParameters);

            var keyPair = generator.GenerateKeyPair();
            var privateKeyParameters = keyPair.Private as RsaPrivateCrtKeyParameters;
           // var publicKeyParameters = keyPair.Public as RsaPublicKeyParameters;


            var privateKeyParametersJson = JsonConvert.SerializeObject(PrivateKeyToString(privateKeyParameters));
            //var publicKeyParametersJson = JsonConvert.SerializeObject(keyPair.Public);

            return (privateKeyParametersJson);
        }

        private string PrivateKeyToString(RsaPrivateCrtKeyParameters privateKeyParameters)
        {
            var privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKeyParameters);
            var privateKeyBytes = privateKeyInfo.ToAsn1Object().GetDerEncoded();
            return Convert.ToBase64String(privateKeyBytes);
        }

        //private string PublicKeyToString(RsaPublicKeyParameters publicKeyParameters)
        //{
        //    var subjectPublicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKeyParameters);
        //    var publicKeyBytes = subjectPublicKeyInfo.ToAsn1Object().GetDerEncoded();
        //    return Convert.ToBase64String(publicKeyBytes);
        //}
    }
}
