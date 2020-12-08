using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagementWeb.Models.DTO
{
    public class UserStaffDTO
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
