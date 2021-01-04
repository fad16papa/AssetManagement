using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AssetManagementWeb.Models;
using AssetManagementWeb.Repositories.Interfaces;

namespace AssetManagementWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAssetInterface _assetInterface;
        private readonly IUserStaffInterface _userStaffInterface;
        private readonly IUserAssetsInterface _userAssetsInterface;
        private readonly IUserLicenseInterface _userLicenseInterface;
        private readonly ILicenseInterface _licenseInterface;

        public HomeController(ILogger<HomeController> logger, IAssetInterface assetInterface, IUserStaffInterface userStaffInterface, IUserAssetsInterface userAssetsInterface,
        IUserLicenseInterface userLicenseInterface, ILicenseInterface licenseInterface)
        {
            _licenseInterface = licenseInterface;
            _userLicenseInterface = userLicenseInterface;
            _userAssetsInterface = userAssetsInterface;
            _userStaffInterface = userStaffInterface;
            _assetInterface = assetInterface;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (Request.Cookies["AssetReference"] == null)
            {
                return RedirectToAction("Index", "Error");
            }

            ViewBag.YesActive = "Yes";
            ViewBag.NoActive = "No";
            ViewBag.Active = "active";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
