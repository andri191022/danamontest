using Microsoft.Build.ObjectModelRemoting;

namespace Testing.DanamonNew.Utility
{
    public class SD
    {
        public static string RegisterVaAPIBase { get; set; }
        public static string AuthAPIBase { get; set; }

        //BDI

        public static string OACClientID { get; set; }
        public static string OACClientIDSecret { get; set; }
        public static  string BDIKey { get; set; }
        public static string BDIKeySecret { get; set; }

        //


        public const string RoleAdmin = "ADMIN";
        public const string TokenCookie = "JWTToken";
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
    }
}
