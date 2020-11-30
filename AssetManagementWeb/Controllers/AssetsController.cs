using System;
using System.Threading.Tasks;
using AssetManagementWeb.Models;
using AssetManagementWeb.Models.DTO;
using AssetManagementWeb.Repositories.Interfaces;
using Domain;
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

        [HttpPost]
        public async Task<IActionResult> Create(AssetsDTO assetsDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(assetsDTO);
                }

                StatusModel statusModel = (StatusModel)Enum.Parse(typeof(StatusModel), assetsDTO.Status);
                LocationModel locationModel = (LocationModel)Enum.Parse(typeof(LocationModel), assetsDTO.Location);
                TypeModel typeModel = (TypeModel)Enum.Parse(typeof(TypeModel), assetsDTO.Type);
                AvailabilityModel availabilityModel = (AvailabilityModel)Enum.Parse(typeof(AvailabilityModel), assetsDTO.IsAvailable);

                assetsDTO.Status = statusModel.ToString();
                assetsDTO.Location = locationModel.ToString();
                assetsDTO.Type = typeModel.ToString();
                assetsDTO.IsAvailable = availabilityModel.ToString();

                Asset asset = new Asset()
                {
                    Brand = assetsDTO.Brand,
                    HostName = assetsDTO.HostName,
                    ExpressCode = assetsDTO.ExpressCode,
                    IsAvailable = availabilityModel.ToString(),
                    Location = locationModel.ToString(),
                    Status = statusModel.ToString(),
                    Model = assetsDTO.Model,
                    Remarks = assetsDTO.Remarks,
                    SerialNo = assetsDTO.SerialNo,
                    Type = typeModel.ToString(),
                };

                var result = await _assetInterface.CreateAsset(asset, Request.Cookies["AssetReference"].ToString());

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in AssetsController||Create ErrorMessage: {ex.Message}");
                return RedirectToAction("Index", "Error");
            }
        }
    }
}