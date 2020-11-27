using AssetManagementWeb.Models.ApiResponse;
using AssetManagementWeb.Models.DTO;
using AssetManagementWeb.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AssetManagementWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserInterface _userInterface;
        public UserController(ILogger<UserController> logger, IUserInterface userInterface)
        {
            _userInterface = userInterface;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var response = await _userInterface.Login(loginDTO);

            if(!response.GetType().GetProperty("ResponseCode").GetValue(response, null).Equals("OK"))
            {
                return View();
            }

            //Create a cookies for jwt token 
            var cookies = new CookieOptions();
            cookies.Expires = DateTime.Now.AddDays(1);
            cookies.HttpOnly = true;

            Response.Cookies.Append("Token", response.GetType().GetProperty("Token").GetValue(response, null).ToString(), cookies);
            Response.Cookies.Append("UserName", response.GetType().GetProperty("UserName").GetValue(response, null).ToString(), cookies);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("Token");
            Response.Cookies.Delete("UserName");

            return RedirectToAction("Login", "User");
        }
    }
}