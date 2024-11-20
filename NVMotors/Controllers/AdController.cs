using Microsoft.AspNetCore.Mvc;

namespace NVMotors.Web.Controllers
{
    public class AdController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
