using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagementWeb.Models.DTO;
using AssetManagementWeb.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementWeb.Components
{
    public class AssetsComponent : ViewComponent
    {
        private readonly IAssetInterface _assetInterface;
        private readonly IMapper _mapper;
        public AssetsComponent(IAssetInterface assetInterface, IMapper mapper)
        {
            _mapper = mapper;
            _assetInterface = assetInterface;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _assetInterface.GetAssets(Request.Cookies["AssetReference"].ToString());

            //Map the objects results to corresponding DTO's
            IEnumerable<AssetsDTO> assetsDTOs = _mapper.Map<IEnumerable<AssetsDTO>>(result);

            ViewBag.TotalAssetsCount = assetsDTOs.ToList().Count();

            return View(assetsDTOs);
        }
    }
}