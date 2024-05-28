using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService2.Model.Dto
{
    public class AuthResponseB2BDto
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public string accessToken { get; set; }
        public string expiresIn { get; set; }
        public string tokenType { get; set; }
    }
}
