using AssetManagementWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagementWeb.Models.ViewModel
{
    public class ViewAssetsUserViewModel
    {
        public AssetsDTO AssetsDTO { get; set; }
        public List<UserAssetsDTO> UserAssetsDTOs { get; set; }
    }
}
