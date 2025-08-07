using System.ComponentModel.DataAnnotations;

namespace JazzcashPortal.Models.Setups
{
    public class ProductConfiguration
    {
        public string? PRODUCT_ID { get; set; }

        [Required(ErrorMessage = "Please enter product name")]
        public string? PRODUCT_NAME { get; set; }

        [Required(ErrorMessage = "Please enter sum covered")]
        public string? SUMCOVERED { get; set; }

        [Required(ErrorMessage = "Please enter net contribution")]
        public string? NET_CONTRIBUTION { get; set; }
    }
}
