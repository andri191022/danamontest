using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService2.Model.Dto
{
    public class HeaderDto
    {
        public string BDI_Signature { get; set; }
        public string BDI_Key { get; set; }
        public string BDI_Timestamp { get; set; }
        public string Authorization { get; set; }
    }
}
