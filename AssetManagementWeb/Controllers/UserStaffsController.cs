using AssetManagementWeb.Helper;
using AssetManagementWeb.Models;
using AssetManagementWeb.Models.DTO;
using AssetManagementWeb.Models.ViewModel;
using AssetManagementWeb.Repositories.Interfaces;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagementWeb.Controllers
{
    public class UserStaffsController : Controller
    {
        private readonly ILogger<UserStaffsController> _logger;
        private readonly IUserStaffInterface _userStaffInterface;
        private readonly IUserAssetsInterface _userAssetsInterface;
        private readonly IUserLicenseInterface _userLicenseInterface;
        private readonly IMapper _mapper;

        public UserStaffsController(ILogger<UserStaffsController> logger, IUserStaffInterface userStaffInterface, IUserAssetsInterface userAssetsInterface,
        IUserLicenseInterface userLicenseInterface, IMapper mapper)
        {
            _mapper = mapper;
            _userLicenseInterface = userLicenseInterface;
            _userAssetsInterface = userAssetsInterface;
            _logger = logger;
            _userStaffInterface = userStaffInterface;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string paramStatus, string token, string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            try
            {
                ViewData["CurrentSort"] = sortOrder;
                ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "HostName" : "";
                ViewData["DateSortParm"] = sortOrder == "AssetNo" ? "ExpressCode" : "AssetNo";

                if (searchString != null)
                {
                    pageNumber = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                var listUserStaff = new List<UserStaffDTO>();

                listUserStaff = await _userStaffInterface.GetUserStaffs(Request.Cookies["AssetReference"].ToString());

                var model = listUserStaff.AsQueryable();

                if (!String.IsNullOrEmpty(searchString))
                {
                    model = listUserStaff.AsQueryable().Where(x => x.DisplayName.Contains(searchString) || x.Department.Equals(searchString) || x.Location.Equals(searchString));
                }

                switch (sortOrder)
                {
                    case "DisplayName":
                        model = model.OrderByDescending(s => s.DisplayName);
                        break;
                    case "Department":
                        model = model.OrderByDescending(s => s.Department);
                        break;
                    case "Location":
                        model = model.OrderByDescending(s => s.Location);
                        break;
                    default:
                        model = model.OrderBy(s => s.DisplayName);
                        break;
                }

                int pageSize = 10;
                return View(await PaginatedList<UserStaffDTO>.CreateAsync(model.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in UserStaffsController||Index ErrorMessage: {ex.Message}");
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ViewUserStaff(string UserStaffId)
        {
            try
            {
                if (Request.Cookies["AssetReference"] == null)
                {
                    return RedirectToAction("Index", "Error");
                }

                var result = await _userStaffInterface.GetUserStaff(UserStaffId, Request.Cookies["AssetReference"].ToString());

                if (result == null)
                {
                    return RedirectToAction("Index", "Error");
                }

                var userLicense = await _userLicenseInterface.GetUserOfLicense(UserStaffId, Request.Cookies["AssetReference"].ToString());

                if (userLicense == null)
                {
                    return RedirectToAction("Index", "Error");
                }

                var userAssets = await _userAssetsInterface.GetUserOfAssets(UserStaffId, Request.Cookies["AssetReference"].ToString());

                if (userAssets == null)
                {
                    return RedirectToAction("Index", "Error");
                }

                //Map the objects results to corresponding DTO's
                UserStaffDTO userStaffDTO = _mapper.Map<UserStaffDTO>(result);
                List<UserAssets> listUserAssets = _mapper.Map<List<UserAssets>>(userAssets);
                List<UserLicense> listUserLicenses = _mapper.Map<List<UserLicense>>(userLicense);

                //Instantiate UserAssetsLicenseViewModel 
                var userAssetsLicenseViewModel = new UserAssetsLicenseViewModel()
                {
                    UserStaffDTO = userStaffDTO,
                    UserLicense = listUserLicenses,
                    UserAssets = listUserAssets
                };

                return View(userAssetsLicenseViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in UserStaffsController||ViewUserStaff ErrorMessage: {ex.Message}");
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
                _logger.LogError($"Error encountered in UserStaffsController||Create ErrorMessage: {ex.Message}");
                return RedirectToAction("Index", "Error");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(UserStaffDTO userStaffDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(userStaffDTO);
                }

                LocationModel locationModel = (LocationModel)Enum.Parse(typeof(LocationModel), userStaffDTO.Location);
                AvailabilityModel availabilityModel = (AvailabilityModel)Enum.Parse(typeof(AvailabilityModel), userStaffDTO.IsActive);

                var userStaff = new UserStaff()
                {
                    DisplayName = userStaffDTO.DisplayName,
                    Department = userStaffDTO.Department,
                    Location = locationModel.ToString(),
                    IsActive = availabilityModel.ToString(),
                    DateCreated = userStaffDTO.DateCreated
                };

                var result = await _userStaffInterface.CreateUserStaff(userStaff, Request.Cookies["AssetReference"].ToString());

                return View(userStaffDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in UserStaffsController||Create ErrorMessage: {ex.Message}");
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(string userStaffId)
        {
            try
            {
                if (Request.Cookies["AssetReference"] == null)
                {
                    return RedirectToAction("Index", "Error");
                }

                var userStaff = await _userStaffInterface.GetUserStaff(userStaffId, Request.Cookies["AssetReference"].ToString());

                return View(userStaff);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in UserStaffsController||Update ErrorMessage: {ex.Message}");
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserStaffDTO userStaffDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(userStaffDTO);
                }

                LocationModel locationModel = (LocationModel)Enum.Parse(typeof(LocationModel), userStaffDTO.Location);
                AvailabilityModel availabilityModel = (AvailabilityModel)Enum.Parse(typeof(AvailabilityModel), userStaffDTO.IsActive);

                var userStaff = new UserStaff()
                {
                    Id = userStaffDTO.Id,
                    DisplayName = userStaffDTO.DisplayName,
                    Department = userStaffDTO.Department,
                    Location = locationModel.ToString(),
                    IsActive = availabilityModel.ToString(),
                    DateCreated = userStaffDTO.DateCreated
                };

                var result = await _userStaffInterface.EditUserStaff(userStaff, Request.Cookies["AssetReference"].ToString());

                return View(userStaffDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in UserStaffsController||Update ErrorMessage: {ex.Message}");
                return RedirectToAction("Index", "Error");
            }
        }
    }
}