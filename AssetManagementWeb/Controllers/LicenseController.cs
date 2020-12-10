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
            try
            {
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

                AvailabilityModel availabilityModel = (AvailabilityModel)Enum.Parse(typeof(AvailabilityModel), licenseDTO.Expiration);

                var license = new License()
                {
                    ProductName = licenseDTO.ProductName,
                    ProductVersion = licenseDTO.ProductVersion,
                    LicenseKey = licenseDTO.LicenseKey,
                    Expiration = availabilityModel.ToString(),
                    ExpiredOn = licenseDTO.ExpiredOn,
                    Remarks = licenseDTO.Remarks
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

                AvailabilityModel availabilityModel = (AvailabilityModel)Enum.Parse(typeof(AvailabilityModel), licenseDTO.Expiration);

                var license = new License()
                {
                    Id = licenseDTO.Id,
                    ProductName = licenseDTO.ProductName,
                    ProductVersion = licenseDTO.ProductVersion,
                    LicenseKey = licenseDTO.LicenseKey,
                    Expiration = availabilityModel.ToString(),
                    ExpiredOn = licenseDTO.ExpiredOn,
                    Remarks = licenseDTO.Remarks
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
    }
}