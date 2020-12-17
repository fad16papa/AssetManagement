using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagementWeb.Models.DTO
{
    public class UserAssetsDTO
    {
        public Guid AssetsId { get; set; }
        public virtual AssetsDTO AssetsDTO { get; set; }
        public Guid UserStaffId { get; set; }
        public virtual UserStaffDTO UserStaffDTO { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ReturnedOn { get; set; }
    }
}
