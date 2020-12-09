using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AssetManagementWeb.Controllers
{
    public class LicenseController : Controller
    {
        private readonly ILogger<LicenseController> _logger;
        public LicenseController(ILogger<LicenseController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}