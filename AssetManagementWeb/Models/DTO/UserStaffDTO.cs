using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagementWeb.Models.DTO
{
    public class UserStaffDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string IsActive { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
