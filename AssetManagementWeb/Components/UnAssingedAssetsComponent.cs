using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagementWeb.Models.DTO;
using AssetManagementWeb.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementWeb.Components
{
    public class UnAssingedAssetsComponent : ViewComponent
    {
        private readonly IMapper _mapper;
        private readonly IAssetInterface _assetInterface;
        public UnAssingedAssetsComponent(IAssetInterface assetInterface, IMapper mapper)
        {
            _assetInterface = assetInterface;
            _mapper = mapper;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _assetInterface.GetAssets(Request.Cookies["AssetReference"].ToString());

            //Map the objects results to corresponding DTO's
            IEnumerable<AssetsDTO> assetsDTOs = _mapper.Map<IEnumerable<AssetsDTO>>(result);

            var listUnAsssignedAssets = assetsDTOs.Where(x => x.IsAssinged == "No").ToList();

            ViewBag.TotalUnAssignedAssetsCount = listUnAsssignedAssets.Count();

            return View(listUnAsssignedAssets);
        }
    }
}