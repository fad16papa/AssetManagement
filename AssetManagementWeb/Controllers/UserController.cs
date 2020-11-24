using AssetManagementWeb.Models.ApiResponse;
using AssetManagementWeb.Models.DTO;
using AssetManagementWeb.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

            if(response.GetType().GetProperty("ResponseCode").GetValue(response, null).Equals("Ok"))
            {

            }

            return View();
        }
    }
}