using System;
using System.Collections.Generic;
using AssetManagementWeb.Models.DTO;
using Domain;

namespace AssetManagementWeb.Models.ViewModel
{
    public class LicenseAssetsViewModel
    {
        public Guid AssetId { get; set; }
        public List<AssetsDTO> AssetsDTOs { get; set; }
        public Guid LicenseId { get; set; }
        public LicenseDTO LicenseDTO { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ReturnedOn { get; set; }
    }
}