using Microsoft.Build.ObjectModelRemoting;
using System.Security.Cryptography;

namespace Testing.DanamonNew.Utility
{
    public class SD
    {
        public static RSAParameters RSAKey { get;set; }
        public static string RegisterVaAPIBase { get; set; }
        public static string AuthAPIBase { get; set; }

        //BDI

        public static string OACClientID { get; set; }
        public static string OACClientIDSecret { get; set; }
        public static  string BDIKey { get; set; }
        public static string BDIKeySecret { get; set; }

        //


        public const string RoleAdmin = "ADMIN";
        public const string TokenCookie = "TS01f29741";
        public const string AuthCookie = "JWTToken";
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }      

        public enum ContentType
        {
            Json,
            MultipartFormData,
        }

        public enum FunctionDBIType
        {
            SC_73, //authentication token
            SC_B1, //Inquiry Multifinance API
            SC_B2,  //Payment Multifinance API
            SC_24, //Virtual Accunt Inquiry as Client(SNAP BI) API
            SC_25 //Virtual Account Payment as Client(SNAP BI) API
        }
    }
}
