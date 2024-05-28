using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService2.Utility
{
    public class SD
    {
        public static string? AuthAPIBase { get; set; }
        public static string? AuthB2BAPIBase { get; set; }

        //BDI
        public static string? OACClientID { get; set; }
        public static string? OACClientIDSecret { get; set; }
        public static string? BDIKey { get; set; }
        public static string? BDIKeySecret { get; set; }

        //

        public static string? JktProxy {  get; set; }
        public static int? JktPort {  get; set; }
        public static int? JktUseProxy {  get; set; }

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
