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
    public class LicenseController : Controller
    {
        private readonly ILogger<LicenseController> _logger;
        private readonly ILicenseInterface _licenseInterface;
        private readonly IMapper _mapper;
        private readonly IUserLicenseInterface _userLicenseInterface;
        private readonly IUserStaffInterface _userStaffInterface;
        private readonly IAssetsLicenseInterface _assetsLicenseInterface;
        private readonly IAssetInterface _assetInterface;
        public LicenseController(ILogger<LicenseController> logger, ILicenseInterface licenseInterface, IMapper mapper, IUserLicenseInterface userLicenseInterface,
        IUserStaffInterface userStaffInterface, IAssetsLicenseInterface assetsLicenseInterface, IAssetInterface assetInterface)
        {
            _userStaffInterface = userStaffInterface;
            _assetInterface = assetInterface;
            _assetsLicenseInterface = assetsLicenseInterface;
            _userLicenseInterface = userLicenseInterface;
            _mapper = mapper;
            _licenseInterface = licenseInterface;
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

                var result = await _licenseInterface.GetLicenses(Request.Cookies["AssetReference"].ToString());

                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in LicenseController||Index ErrorMessage: {ex.Message}");
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ViewLicense(string LicenseId)
        {
            try
            {
                if (Request.Cookies["AssetReference"] == null)
                {
                    return RedirectToAction("Index", "Error");
                }

                var license = await _licenseInterface.GetLicense(LicenseId, Request.Cookies["AssetReference"].ToString());

                if (license == null)
                {
                    return RedirectToAction("Index", "Error");
                }

                var userLicense = await _userLicenseInterface.GetLicensesOfUser(LicenseId, Request.Cookies["AssetReference"].ToString());

                if (userLicense == null)
                {
                    return RedirectToAction("Index", "Error");
                }

                var assetsLicense = await _assetsLicenseInterface.GetLicenseOfAssets(LicenseId, Request.Cookies["AssetReference"].ToString());

                if (assetsLicense == null)
                {
                    return RedirectToAction("Index", "Error");
                }

                //Map the objects results to corresponding DTO's
                LicenseDTO licenseDTO = _mapper.Map<LicenseDTO>(license);
                List<UserLicense> userLicenses = _mapper.Map<List<UserLicense>>(userLicense);
                List<AssetsLicense> assetsLicenses = _mapper.Map<List<AssetsLicense>>(assetsLicense);

                //Instantiate AssetsUserVIewModel 
                var viewLicenseUserViewModel = new ViewLicenseUserViewModel()
                {
                    LicenseDTO = licenseDTO,
                    UserLicenses = userLicenses,
                    AssetsLicenses = assetsLicenses
                };

                return View(viewLicenseUserViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in LicenseController||ViewLicense ErrorMessage: {ex.Message}");
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

                //Set the Date to its initial value
                var date = DateTime.Now;
                ViewBag.Date = date.ToString("yyyy-MM-dd");

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in LicenseController||Create ErrorMessage: {ex.Message}");
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(LicenseDTO licenseDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(licenseDTO);
                }

                AvailabilityModel expiration = (AvailabilityModel)Enum.Parse(typeof(AvailabilityModel), licenseDTO.Expiration);
                AvailabilityModel availabilityModel = (AvailabilityModel)Enum.Parse(typeof(AvailabilityModel), licenseDTO.IsAvailable);

                if (licenseDTO.Expiration.Equals("No"))
                {
                    licenseDTO.ExpiredOn = DateTime.MinValue;
                }

                var license = new License()
                {
                    ProductName = licenseDTO.ProductName,
                    ProductVersion = licenseDTO.ProductVersion,
                    LicenseKey = licenseDTO.LicenseKey,
                    Expiration = expiration.ToString(),
                    ExpiredOn = licenseDTO.ExpiredOn,
                    Remarks = licenseDTO.Remarks,
                    IsAvailable = availabilityModel.ToString(),
                    IsAssigned = "No"
                };

                var result = await _licenseInterface.CreateLicense(license, Request.Cookies["AssetReference"].ToString());

                return View(licenseDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in LicenseController||Create ErrorMessage: {ex.Message}");
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(string licenseId)
        {
            try
            {
                if (Request.Cookies["AssetReference"] == null)
                {
                    return RedirectToAction("Index", "Error");
                }

                var license = await _licenseInterface.GetLicense(licenseId, Request.Cookies["AssetReference"].ToString());

                return View(license);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in LicenseController||Update ErrorMessage: {ex.Message}");
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(LicenseDTO licenseDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(licenseDTO);
                }

                AvailabilityModel expiration = (AvailabilityModel)Enum.Parse(typeof(AvailabilityModel), licenseDTO.Expiration);
                AvailabilityModel availabilityModel = (AvailabilityModel)Enum.Parse(typeof(AvailabilityModel), licenseDTO.IsAvailable);
                AvailabilityModel assigned = (AvailabilityModel)Enum.Parse(typeof(AvailabilityModel), licenseDTO.IsAssigned);

                var license = new License()
                {
                    Id = licenseDTO.Id,
                    ProductName = licenseDTO.ProductName,
                    ProductVersion = licenseDTO.ProductVersion,
                    LicenseKey = licenseDTO.LicenseKey,
                    Expiration = expiration.ToString(),
                    ExpiredOn = licenseDTO.ExpiredOn,
                    Remarks = licenseDTO.Remarks,
                    IsAvailable = availabilityModel.ToString(),
                    IsAssigned = assigned.ToString()
                };

                var result = await _licenseInterface.EditLicense(license, Request.Cookies["AssetReference"].ToString());

                return View(licenseDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in AssetsController||Update ErrorMessage: {ex.Message}");
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AssignLicenseUser(string licenseId)
        {
            try
            {
                var license = await _licenseInterface.GetLicense(licenseId, Request.Cookies["AssetReference"].ToString());

                if (license == null)
                {
                    return RedirectToAction("Index", "Error");
                }

                var user = await _userStaffInterface.GetUserStaffs(Request.Cookies["AssetReference"].ToString());

                if (user == null)
                {
                    return RedirectToAction("Index", "Error");
                }

                //Map the objects results to corresponding DTO's
                LicenseDTO licenseDTO = _mapper.Map<LicenseDTO>(license);
                List<UserStaffDTO> userStaffDTO = _mapper.Map<List<UserStaffDTO>>(user);

                //Instantiate LicenseUserViewModel
                var licenseUserViewModel = new LicenseUserViewModel()
                {
                    LicenseDTO = licenseDTO,
                    UserStaffDTOs = userStaffDTO
                };

                //Set the Date to its initial value
                var date = DateTime.Now;
                ViewBag.Date = date.ToString("yyyy-MM-dd");

                ViewBag.LicenseId = licenseUserViewModel.LicenseDTO.Id;

                return View(licenseUserViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in LicenseController||AssignLicenseUser ErrorMessage: {ex.Message}");
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AssignLicenseUser(LicenseUserViewModel licenseUserViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(licenseUserViewModel);
                }

                //Check if the user is already assigned in target asset 
                var checkUser = await _userLicenseInterface.GetUserOfLicense(licenseUserViewModel.UserStaffId.ToString(), Request.Cookies["AssetReference"].ToString());

                var userLicense = new UserLicense()
                {
                    LicenseId = licenseUserViewModel.LicenseId,
                    UserStaffId = licenseUserViewModel.UserStaffId,
                    IssuedOn = licenseUserViewModel.IssuedOn,
                    ReturnedOn = licenseUserViewModel.ReturedOn,
                    IsActive = "Yes"
                };

                var result = await _userLicenseInterface.CreateUserLicense(userLicense, Request.Cookies["AssetReference"].ToString());

                if (result.ResponseCode != HttpStatusCode.OK.ToString())
                {
                    ViewBag.ErrorResponse = result.ResponseMessage;
                    return View();
                }

                var license = new License()
                {
                    Id = licenseUserViewModel.LicenseId,
                    IsAssigned = "Yes"
                };

                var response = await _licenseInterface.EditLicense(license, Request.Cookies["AssetReference"].ToString());

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
                LicenseUserViewModel model = new LicenseUserViewModel()
                {
                    UserStaffDTOs = userStaffDTO
                };

                ViewBag.LicenseId = userLicense.LicenseId;

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in LicenseController||AssignLicenseUser ErrorMessage: {ex.Message}");
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AssignLicenseAsset(string licenseId)
        {
            try
            {
                var license = await _licenseInterface.GetLicense(licenseId, Request.Cookies["AssetReference"].ToString());

                if (license == null)
                {
                    return RedirectToAction("Index", "Error");
                }

                var assets = await _assetInterface.GetAssets(Request.Cookies["AssetReference"].ToString());

                if (assets == null)
                {
                    return RedirectToAction("Index", "Error");
                }

                //Map the objects results to corresponding DTO's
                LicenseDTO licenseDTO = _mapper.Map<LicenseDTO>(license);
                List<AssetsDTO> assetsDTOs = _mapper.Map<List<AssetsDTO>>(assets);

                //Instantiate LicenseAssetsViewModel
                var licenseAssetsViewModel = new LicenseAssetsViewModel()
                {
                    LicenseDTO = licenseDTO,
                    AssetsDTOs = assetsDTOs
                };

                //Set the Date to its initial value
                var date = DateTime.Now;
                ViewBag.Date = date.ToString("yyyy-MM-dd");

                ViewBag.LicenseId = licenseAssetsViewModel.LicenseDTO.Id;

                return View(licenseAssetsViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in LicenseController||AssignLicenseAsset ErrorMessage: {ex.Message}");
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AssignLicenseAsset(LicenseAssetsViewModel licenseAssetsViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(licenseAssetsViewModel);
                }

                //Check if the asset is already assigned in target asset 
                var checkUser = await _assetsLicenseInterface.GetAssetsOfLicense(licenseAssetsViewModel.AssetId.ToString(), Request.Cookies["AssetReference"].ToString());

                var assetsLicense = new AssetsLicense()
                {
                    LicenseId = licenseAssetsViewModel.LicenseId,
                    AssetId = licenseAssetsViewModel.AssetId,
                    IssuedOn = licenseAssetsViewModel.IssuedOn,
                    ReturnedOn = licenseAssetsViewModel.ReturnedOn,
                    IsActive = "Yes"
                };

                var result = await _assetsLicenseInterface.CreateAssetsLicense(assetsLicense, Request.Cookies["AssetReference"].ToString());

                if (result.ResponseCode != HttpStatusCode.OK.ToString())
                {
                    ViewBag.ErrorResponse = result.ResponseMessage;
                    return View();
                }

                var license = new License()
                {
                    Id = licenseAssetsViewModel.LicenseId,
                    IsAssigned = "Yes"
                };

                var response = await _licenseInterface.EditLicense(license, Request.Cookies["AssetReference"].ToString());

                if (response.ResponseCode != HttpStatusCode.OK.ToString())
                {
                    return RedirectToAction("Index", "Error");
                }

                var assets = await _assetInterface.GetAssets(Request.Cookies["AssetReference"].ToString());

                if (assets == null)
                {
                    return RedirectToAction("Index", "Error");
                }

                //Map the objects results to corresponding DTO's
                List<AssetsDTO> assetsDTOs = _mapper.Map<List<AssetsDTO>>(assets);

                //Instantiate AssetsUserVIewModel
                LicenseAssetsViewModel model = new LicenseAssetsViewModel()
                {
                    AssetsDTOs = assetsDTOs
                };

                ViewBag.LicenseId = assetsLicense.LicenseId;

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in LicenseController||AssignLicenseAsset ErrorMessage: {ex.Message}");
                return RedirectToAction("Index", "Error");
            }
        }
    }
}