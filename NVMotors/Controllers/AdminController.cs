using Microsoft.AspNetCore.Mvc;

namespace NVMotors.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
