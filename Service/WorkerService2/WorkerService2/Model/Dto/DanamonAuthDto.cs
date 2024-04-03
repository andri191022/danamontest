using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService2.Model.Dto
{
    public class DanamonAuthDto
    {
        [Required]
        public string Authorization { get; set; }
        [Required]
        public string grant_type { get; set; }
    }
}
