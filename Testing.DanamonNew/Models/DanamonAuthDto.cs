using System.ComponentModel.DataAnnotations;

namespace Testing.DanamonNew.Models
{
    public class DanamonAuthDto
    {
        [Required]
        public string Authorization { get; set; }
        [Required]
        public string grant_type { get; set; }
        
    }
}
