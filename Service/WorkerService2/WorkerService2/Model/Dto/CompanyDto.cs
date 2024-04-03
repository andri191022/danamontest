using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService2.Model.Dto
{
    public class CompanyDto
    {
        public int count { get; set; }
        public List<tb_Company> entries { get; set; }
    }
}
