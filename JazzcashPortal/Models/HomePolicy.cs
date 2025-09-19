using System.ComponentModel.DataAnnotations;

namespace JazzcashPortal.Models
{
    public class HomePolicy
    {
        [Required(ErrorMessage = "Please Enter From Date")]
        public string? PERIOD_FROM { get; set; }

        [Required(ErrorMessage = "Please Enter To Date")]
        public string? PERIOD_TO { get; set; }
        public string? ContactNo { get; set; }
    }
}
