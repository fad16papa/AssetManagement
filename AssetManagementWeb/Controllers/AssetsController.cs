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
        private readonly IUserInterface _userInterface;

        public AssetsController(ILogger<AssetsController> logger, IAssetInterface assetInterface, IUserInterface userInterface)
        {
            _assetInterface = assetInterface;
            _userInterface = userInterface;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _assetInterface.GetAssets(Request.Cookies["AssetReference"].ToString());

                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in AssetsController||Index ErrorMessage: {ex.Message}");
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                if (Request.Cookies["AssetReference"] == null)
                {
                    return RedirectToAction("Index", "Error");
                }

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in AssetsController||Create ErrorMessage: {ex.Message}");
                return RedirectToAction("Index", "Error");
            }
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

                return View(assetsDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in AssetsController||Create ErrorMessage: {ex.Message}");
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(string AssetId)
        {
            try
            {
                if (Request.Cookies["AssetReference"] == null)
                {
                    return RedirectToAction("Index", "Error");
                }

                var asset = await _assetInterface.GetAsset(AssetId, Request.Cookies["AssetReference"].ToString()); 

                return View(asset);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in AssetsController||Create ErrorMessage: {ex.Message}");
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(AssetsDTO assetsDTO)
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
                    Id = assetsDTO.Id,
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

                var result = await _assetInterface.EditAsset(asset, Request.Cookies["AssetReference"].ToString());

                return View(assetsDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in AssetsController||Update ErrorMessage: {ex.Message}");
                return RedirectToAction("Index", "Error");
            }
        }
    }
}