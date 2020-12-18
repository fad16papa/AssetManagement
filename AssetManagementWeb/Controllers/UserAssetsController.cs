using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssetManagementWeb.Models.DTO;
using AssetManagementWeb.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AssetManagementWeb.Controllers
{
    public class UserAssetsController : Controller
    {
        private readonly IUserAssetsInterface _userAssetsInterface;
        private readonly ILogger<UserAssetsController> _logger;
        public UserAssetsController(ILogger<UserAssetsController> logger, IUserAssetsInterface userAssetsInterface)
        {
            _logger = logger;
            _userAssetsInterface = userAssetsInterface;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var userAssets = await _userAssetsInterface.GetUserAssets(Request.Cookies["AssetReference"].ToString());

                var resultAsset = await _userAssetsInterface.GetUserOfAssets("2e02d462-6e82-4146-affa-08d89be9abb5", Request.Cookies["AssetReference"].ToString());

                var resultUser = await _userAssetsInterface.GetAssetsOfUser("6d0b63c2-b12d-40d1-4f40-08d8959ebd0b", Request.Cookies["AssetReference"].ToString());

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in UserAssetsController||Index ErrorMessage: {ex.Message}");
                return RedirectToAction("Index", "Error");
            }
        }
    }
}