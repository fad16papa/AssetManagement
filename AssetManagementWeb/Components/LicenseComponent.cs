using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagementWeb.Models.DTO;
using AssetManagementWeb.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementWeb.Components
{
    public class LicenseComponent : ViewComponent
    {
        private readonly ILicenseInterface _licenseInterface;
        private readonly IMapper _mapper;
        public LicenseComponent(ILicenseInterface licenseInterface, IMapper mapper)
        {
            _mapper = mapper;
            _licenseInterface = licenseInterface;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _licenseInterface.GetLicenses(Request.Cookies["AssetReference"].ToString());

            //Map the objects results to corresponding DTO's
            IEnumerable<LicenseDTO> licenseDTOs = _mapper.Map<IEnumerable<LicenseDTO>>(result);

            ViewBag.TotalLicensesCount = licenseDTOs.ToList().Count();

            return View(licenseDTOs);
        }
    }
}