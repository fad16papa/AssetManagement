using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AssetManagementWeb.Models;
using AssetManagementWeb.Models.DTO;
using AssetManagementWeb.Models.ViewModel;
using AssetManagementWeb.Repositories.Interfaces;
using AutoMapper;
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
        private readonly IUserStaffInterface _userStaffInterface;
        private readonly IMapper _mapper;
        private readonly IUserAssetsInterface _userAssetsInterface;

        public AssetsController(ILogger<AssetsController> logger, IAssetInterface assetInterface, IUserInterface userInterface, IUserStaffInterface userStaffInterface,
            IMapper mapper, IUserAssetsInterface userAssetsInterface)
        {
            _assetInterface = assetInterface;
            _userInterface = userInterface;
            _userStaffInterface = userStaffInterface;
            _mapper = mapper;
            _userAssetsInterface = userAssetsInterface;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                if (Request.Cookies["AssetReference"] == null)
                {
                    return RedirectToAction("Index", "Error");
                }

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
        public async Task<IActionResult> ViewAsset(string assetId)
        {
            try
            {
                if (Request.Cookies["AssetReference"] == null)
                {
                    return RedirectToAction("Index", "Error");
                }

                var asset = await _assetInterface.GetAsset(assetId, Request.Cookies["AssetReference"].ToString());

                if (asset == null)
                {
                    return RedirectToAction("Index", "Error");
                }

                var userAssets = await _userAssetsInterface.GetAssetsOfUser(assetId, Request.Cookies["AssetReference"].ToString());

                if (userAssets == null)
                {
                    return RedirectToAction("Index", "Error");
                }

                //Map the objects results to corresponding DTO's
                AssetsDTO assetsDTO = _mapper.Map<AssetsDTO>(asset);
                List<UserAssets> userAssetsDTOs = _mapper.Map<List<UserAssets>>(userAssets);

                //Instantiate AssetsUserVIewModel 
                var viewAssetsUserViewModel = new ViewAssetsUserViewModel()
                {
                    AssetsDTO = assetsDTO,
                    UserAssets = userAssetsDTOs
                };

                return View(viewAssetsUserViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in AssetsController||ViewAsset ErrorMessage: {ex.Message}");
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

        [HttpGet]
        public async Task<IActionResult> Update(string assetId)
        {
            try
            {
                if (Request.Cookies["AssetReference"] == null)
                {
                    return RedirectToAction("Index", "Error");
                }

                var asset = await _assetInterface.GetAsset(assetId, Request.Cookies["AssetReference"].ToString());

                return View(asset);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in AssetsController||Update ErrorMessage: {ex.Message}");
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AssignAssetsUsers(string assetId)
        {
            try
            {
                var asset = await _assetInterface.GetAsset(assetId, Request.Cookies["AssetReference"].ToString());

                if (asset == null)
                {
                    return RedirectToAction("Index", "Error");
                }

                var user = await _userStaffInterface.GetUserStaffs(Request.Cookies["AssetReference"].ToString());

                if (user == null)
                {
                    return RedirectToAction("Index", "Error");
                }

                //Map the objects results to corresponding DTO's
                AssetsDTO assetsDTO = _mapper.Map<AssetsDTO>(asset);
                List<UserStaffDTO> userStaffDTO = _mapper.Map<List<UserStaffDTO>>(user);

                //Instantiate AssetsUserVIewModel
                var assetsUserVIewModel = new AssetsUserVIewModel()
                {
                    AssetsDTO = assetsDTO,
                    UserStaffDTOs = userStaffDTO
                };

                //Set the Date to its initial value
                var date = DateTime.Now;
                ViewBag.Date = date.ToString("yyyy-MM-dd");

                ViewBag.AssetId = assetsUserVIewModel.AssetsDTO.Id;

                return View(assetsUserVIewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in AssetsController||AssignAssetsUsers ErrorMessage: {ex.Message}");
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AssignAssetsUsers(AssetsUserVIewModel assetsUserVIewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(assetsUserVIewModel);
                }

                //Check if the user is already assigned in target asset 
                var checkUser = await _userAssetsInterface.GetUserOfAssets(assetsUserVIewModel.UserStaffId.ToString(), Request.Cookies["AssetReference"].ToString());

                var userAssets = new UserAssets()
                {
                    AssetsId = assetsUserVIewModel.AssetId,
                    UserStaffId = assetsUserVIewModel.UserStaffId,
                    IssuedOn = assetsUserVIewModel.IssuedOn,
                    ReturnedOn = assetsUserVIewModel.ReturedOn,
                    IsActive = "Yes", 
                };

                var result = await _userAssetsInterface.CreateUserAssets(userAssets, Request.Cookies["AssetReference"].ToString());

                if (result.ResponseCode != HttpStatusCode.OK.ToString())
                {
                    ViewBag.ErrorResponse = result.ResponseMessage;
                    return View();
                }

                var assets = new Asset()
                {
                    Id = assetsUserVIewModel.AssetId,
                    IsAssinged = "Yes",
                };

                var response = await _assetInterface.EditAsset(assets, Request.Cookies["AssetRefreence"].ToString());

                if (response.ResponseCode != HttpStatusCode.OK.ToString())
                {
                    return RedirectToAction("Index", "Error");
                }

                var user = await _userStaffInterface.GetUserStaffs(Request.Cookies["AssetReference"].ToString());

                if (user == null)
                {
                    return RedirectToAction("Index", "Error");
                }

                //Map the objects results to corresponding DTO's
                List<UserStaffDTO> userStaffDTO = _mapper.Map<List<UserStaffDTO>>(user);

                //Instantiate AssetsUserVIewModel
                AssetsUserVIewModel model = new AssetsUserVIewModel()
                {
                    UserStaffDTOs = userStaffDTO
                };

                ViewBag.AssetId = userAssets.AssetsId;

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in AssetsController||AssignAssetsUsers ErrorMessage: {ex.Message}");
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

                var asset = new Asset()
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