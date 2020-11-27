using Microsoft.AspNetCore.Mvc;

namespace AssetManagementWeb.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}