using AssetManagementWeb.Models.ApiResponse;
using AssetManagementWeb.Models.DTO;
using AssetManagementWeb.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AssetManagementWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserInterface _userInterface;
        private readonly IConfiguration _configuration;

        public UserController(ILogger<UserController> logger, IUserInterface userInterface, IConfiguration configuration)
        {
            _userInterface = userInterface;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            Response.Cookies.Delete("Token");
            Response.Cookies.Delete("UserName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                else
                {
                    var result = await _userInterface.Login(loginDTO);

                    //check the result 
                    //return the view with the error message from the server side
                    if (result.Code == 401)
                    {
                        _logger.LogError($"Error encountered in UserController||Login Error Message {result.Message}");
                        ViewBag.ErrorMessage = result.Message;
                        return View();
                    }

                    //Save the return JWT to sessionStorage
                    var cookieOptions = new CookieOptions()
                    {
                        Expires = DateTime.Now.AddHours(2),
                        IsEssential = true
                    };

                    Response.Cookies.Append(_configuration["AssetCookies:AssetJwt"], result.Token, cookieOptions);
                    Response.Cookies.Append(_configuration["Session:UserName"], result.UserName, cookieOptions);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in UserController||Login ErrorMessage: {ex.Message}");
                return RedirectToAction("Index", "Error");
            }
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("Token");
            Response.Cookies.Delete("UserName");

            return RedirectToAction("Login", "User");
        }
    }
}