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
    public class UserLicenseComponents : ViewComponent
    {
        private readonly IMapper _mapper;
        private readonly IUserLicenseInterface _userLicenseInterface;
        public UserLicenseComponents(IUserLicenseInterface userLicenseInterface, IMapper mapper)
        {
            _userLicenseInterface = userLicenseInterface;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _userLicenseInterface.GetUserLicense(Request.Cookies["AssetReference"].ToString());

            //Map the objects results to corresponding DTO's
            IEnumerable<UserLicense> userLicenses = _mapper.Map<IEnumerable<UserLicense>>(result);

            var resultUserLicense = userLicenses.OrderByDescending(x => x.IssuedOn).Where(x => x.IsActive == "Yes").ToList();

            ViewBag.TotalUserLicenseCount = resultUserLicense.Count();

            return View(resultUserLicense);
        }
    }
}