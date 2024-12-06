using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
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
            var model = context.Ads.Where(a => a.IsApproved == true).Select(a => new AdIndexViewModel
            {
                Id = a.Id,
                Make = a.Motor.Make,
                Model = a.Motor.Model,
                Year = a.Motor.Specification.Year,
                Town = a.Town,
                Price = a.Price,
                ImageURL = a.AdsImages.Select(ai => ai.Image.ImageUrl).FirstOrDefault(),

            });
            return View(model);
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
                MotorId = adModel.Id,
            };
            context.Ads.Add(ad);
            context.SaveChanges();
            return RedirectToAction("AddImages", "AdImage", new { id = ad.Id });
        }
        [HttpGet]
        public IActionResult Details(Guid id)
        { 
            var ad = context.Ads.Include(a => a.Motor).ThenInclude(m => m.MotorCategory).Include(a => a.Motor).ThenInclude(m => m.Specification).Include(a => a.AdsImages).ThenInclude(ai => ai.Image).FirstOrDefault(a => a.Id == id);
            var model = new AdDetailViewModel
            {
                Id = id,
                Category = ad.Motor.MotorCategory.Name,
                Make = ad.Motor.Make,
                Model = ad.Motor.Model,
                Year = ad.Motor.Specification.Year,
                HorsePower = ad.Motor.Specification.HorsePower,
                EngineDisplacement = ad.Motor.Specification.EngineDisplacement,
                TransmissionType = ad.Motor.Specification.TransmissionType,
                FuelType = ad.Motor.Specification.FuelType,
                Color = ad.Motor.Specification.Color,
                Condition = ad.Motor.Specification.Condition,
                Town = ad.Town,
                Description = ad.Description,
                PhoneNumber = ad.PhoneNumber,
                Price = ad.Price,
                ImageURLs = ad.AdsImages.Select(ai => ai.Image.ImageUrl).ToList(),
            };
            return View(model);
        }

    }
}
