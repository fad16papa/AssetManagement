using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagementWeb.Models.DTO;
using AssetManagementWeb.Repositories.Interfaces;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementWeb.Components
{
    public class UserAssetsComponents : ViewComponent
    {
        private readonly IUserAssetsInterface _userAssetsInterface;
        private readonly IMapper _mapper;
        public UserAssetsComponents(IUserAssetsInterface userAssetsInterface, IMapper mapper)
        {
            _mapper = mapper;
            _userAssetsInterface = userAssetsInterface;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _userAssetsInterface.GetUserAssets(Request.Cookies["AssetReference"].ToString());

            //Map the objects results to corresponding DTO's
            IEnumerable<UserAssets> userAssets = _mapper.Map<IEnumerable<UserAssets>>(result);

            var resultUserAssets = userAssets.OrderByDescending(x => x.IssuedOn).Where(x => x.IsActive == "Yes").ToList();

            ViewBag.TotalUserAssetsCount = resultUserAssets.Count();

            return View(resultUserAssets);
        }
    }
}