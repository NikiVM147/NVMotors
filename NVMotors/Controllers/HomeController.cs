using Microsoft.AspNetCore.Mvc;
using NVMotors.Models;
using System.Diagnostics;

namespace NVMotors.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

       public IActionResult Test500()
        {
            try
            {
              
                throw new Exception("Exception");
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        public IActionResult Error(int? statusCode = null)
        {

            if (statusCode == 404)
            {
                return this.View("NotFoundCustom");
            }
            else if (statusCode == 500)
            {
                return this.View("InternalServerErrorCustom");
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
