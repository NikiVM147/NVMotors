using Microsoft.AspNetCore.Mvc;
using NVMotors.Data;
using NVMotors.Data.Models;
using NVMotors.Web.ViewModels.Ad;

namespace NVMotors.Web.Controllers
{
    public class AdController : Controller
    {
        private readonly NVMotorsDbContext context;
        public AdController(NVMotorsDbContext _context)
        {
            context = _context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateAd(Guid id)
        {
            var model = new CreateAdViewModel();
            model.Id = id;
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateAd(CreateAdViewModel adModel)
        {
            if (!ModelState.IsValid)
            {
                return View(adModel);
            }
            var ad = new Ad
            {
                DateAd = DateTime.Now,
                Description = adModel.Description,
                Price = adModel.Price,
                Town = adModel.Town,
                PhoneNumber = adModel.PhoneNumber,
                VechicleId = adModel.Id,
            };
            context.Ads.Add(ad);
            context.SaveChanges();
            return RedirectToAction("AddImages", "AdImage", new { id = ad.Id });
        }
            
        }
}
