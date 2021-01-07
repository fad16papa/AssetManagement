using System.Collections.Generic;
using AssetManagementWeb.Models.DTO;
using Domain;

namespace AssetManagementWeb.Models.ViewModel
{
    public class UserAssetsLicenseViewModel
    {
        public UserStaffDTO UserStaffDTO { get; set; }
        public List<UserLicense> UserLicense { get; set; }
        public List<UserAssets> UserAssets { get; set; }
    }
}