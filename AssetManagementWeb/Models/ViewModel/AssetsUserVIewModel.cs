using AssetManagementWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagementWeb.Models.ViewModel
{
    public class AssetsUserVIewModel
    {
        public AssetsDTO AssetsDTO { get; set; }
        public Guid AssetId { get; set; }
        public Guid UserStaffId { get; set; }
        public List<UserStaffDTO> UserStaffDTOs { get; set; }
        [Required]
        public DateTime IssuedOn { get; set; }
        public DateTime ReturedOn { get; set; }
    }
}
