using AssetManagementWeb.Models;
using AssetManagementWeb.Models.DTO;
using AssetManagementWeb.Repositories.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AssetManagementWeb.Controllers
{
    public class UserStaffsController : Controller
    {
        private readonly ILogger<UserStaffsController> _logger;
        private readonly IUserStaffInterface _userStaffInterface;

        public UserStaffsController(ILogger<UserStaffsController> logger, IUserStaffInterface userStaffInterface)
        {
            _logger = logger;
            _userStaffInterface = userStaffInterface;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _userStaffInterface.GetUserStaffs(Request.Cookies["AssetReference"].ToString());

                return View(result);
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

                return View(result);
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