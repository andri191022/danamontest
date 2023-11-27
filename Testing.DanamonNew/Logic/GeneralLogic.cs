using Microsoft.CodeAnalysis.Operations;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using Testing.DanamonNew.Utility;
using static Testing.DanamonNew.Utility.SD;
using Testing.DanamonNew.Models.Dto;


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
            using (System.Security.Cryptography.SHA256 sha256Hash = System.Security.Cryptography.SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
