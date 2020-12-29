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

        public async Task<IActionResult> Index()
        {
            if (Request.Cookies["AssetReference"] == null)
            {
                return RedirectToAction("Index", "Error");
            }

            var assets = await _assetInterface.GetAssets(Request.Cookies["AssetReference"].ToString());

            if (assets == null)
            {
                assets = 0;
            }

            var license = await _licenseInterface.GetLicenses(Request.Cookies["AssetReference"].ToString());

            if (license == null)
            {
                license = 0;
            }

            var userStaffs = await _userStaffInterface.GetUserStaffs(Request.Cookies["AssetReference"].ToString());

            if (userStaffs == null)
            {
                userStaffs = 0;
            }

            var userAssets = await _userAssetsInterface.GetUserAssets(Request.Cookies["AssetReference"].ToString());

            if (userAssets == null)
            {
                userAssets = 0;
            }

            var userLicenses = await _userLicenseInterface.GetUserLicense(Request.Cookies["AssetReference"].ToString());

            if (userLicenses == null)
            {
                userLicenses = 0;
            }

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
