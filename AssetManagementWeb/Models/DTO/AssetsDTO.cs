using System;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementWeb.Models.DTO
{
    public class AssetsDTO
    {
        public Guid Id { get; set; }
        public string HostName { get; set; }
        [Required]
        public string AssetNo { get; set; }
        [Required]
        public string SerialNo { get; set; }
        public string ExpressCode { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        [Display(Name = "Availability")]
        public string IsAvailable { get; set; }
        public string IsAssinged { get; set; }
        public string Remarks { get; set; }
    }
}