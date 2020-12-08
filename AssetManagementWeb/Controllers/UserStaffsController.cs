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

                var userStaff = new UserStaff()
                {
                    DisplayName = userStaffDTO.DisplayName,
                    Department = userStaffDTO.Department, 
                    Location = userStaffDTO.Location, 
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
    }
}