using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AssetManagementWeb.Models.DTO;

namespace AssetManagementWeb.Models.ViewModel
{
    public class LicenseUserViewModel
    {
        public LicenseDTO LicenseDTO { get; set; }
        public Guid LicenseId { get; set; }
        public Guid UserStaffId { get; set; }
        public List<UserStaffDTO> UserStaffDTOs { get; set; }
        [Required]
        public DateTime IssuedOn { get; set; }
        public DateTime ReturedOn { get; set; }
    }
}