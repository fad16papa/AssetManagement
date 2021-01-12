using System.Collections.Generic;
using AssetManagementWeb.Models.DTO;
using Domain;

namespace AssetManagementWeb.Models.ViewModel
{
    public class ViewLicenseUserViewModel
    {
        public LicenseDTO LicenseDTO { get; set; }
        public List<UserLicense> UserLicenses { get; set; }
        public List<AssetsLicense> AssetsLicenses { get; set; }
    }
}