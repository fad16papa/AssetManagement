using System.Threading.Tasks;
using AssetManagementWeb.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AssetManagementWeb.Controllers
{
    public class LicenseController : Controller
    {
        private readonly ILogger<LicenseController> _logger;
        private readonly ILicenseInterface _licenseInterface;
        public LicenseController(ILogger<LicenseController> logger, ILicenseInterface licenseInterface)
        {
            _licenseInterface = licenseInterface;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _licenseInterface.GetLicenses(Request.Cookies["AssetReference"].ToString());

            return View(result);
        }
    }
}