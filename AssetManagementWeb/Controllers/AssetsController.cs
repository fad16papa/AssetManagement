using AssetManagementWeb.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AssetManagementWeb.Controllers
{
    public class AssetsController : Controller
    {
        private readonly ILogger<AssetsController> _logger;
        private readonly IAssetInterface _assetInterface;
        public AssetsController(ILogger<AssetsController> logger, IAssetInterface assetInterface)
        {
            _assetInterface = assetInterface;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ListAssets()
        {
            _assetInterface.GetAssets(Request.Cookies["Reference"].ToString());

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

    }
}