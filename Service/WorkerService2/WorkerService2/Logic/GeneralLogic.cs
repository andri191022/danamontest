using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static WorkerService2.Utility.SD;
using WorkerService2.Utility;

namespace WorkerService2.Logic
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

    }
}
