using AssetManagementWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagementWeb.Models.VIewModel
{
    public class UserAssetsViewModel
    {
        public Guid AssetsId { get; set; }
        public virtual AssetsDTO Asset { get; set; }
        public Guid UserStaffId { get; set; }
        public virtual UserStaffDTO UserStaff { get; set; }
    }
}
