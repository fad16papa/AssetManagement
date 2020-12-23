using AssetManagementWeb.Models.DTO;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagementWeb.Models.ViewModel
{
    public class ViewAssetsUserViewModel
    {
        public AssetsDTO AssetsDTO { get; set; }
        public List<UserAssets> UserAssets { get; set; }
    }
}
