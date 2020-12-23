using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagementWeb.Models.DTO
{
    public class LicenseDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductVersion { get; set; }
        [Required]
        public string LicenseKey { get; set; }
        [Required]
        public string Expiration { get; set; }
        public DateTime ExpiredOn { get; set; }
        [Required]
        [Display(Name = "Availability")]
        public string IsAvailable { get; set; }
        public string Remarks { get; set; }
    }
}
