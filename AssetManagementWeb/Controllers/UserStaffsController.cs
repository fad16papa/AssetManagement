using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AssetManagementWeb.Controllers
{
    public class UserStaffsController : Controller
    {
        private readonly ILogger<UserStaffsController> _logger;
        public UserStaffsController(ILogger<UserStaffsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}