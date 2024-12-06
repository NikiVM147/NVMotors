using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using NVMotors.Data;
using NVMotors.Data.Models;
using NVMotors.Sevices.Data.Interfaces;
using NVMotors.Web.ViewModels.Ad;

namespace NVMotors.Web.Controllers
{
    public class AdController : Controller
    {
        private readonly NVMotorsDbContext context;
        private readonly IAdService adService;
        public AdController(NVMotorsDbContext _context, IAdService _adService)
        {
            context = _context;
            adService = _adService;
        }
        public IActionResult Index()
        {
            var model = adService.IndexGetAllAds();
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult CreateAd(Guid id)
        {
            var model = new CreateAdViewModel();
            model.Id = id;
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateAd(CreateAdViewModel adModel)
        {
            if (!ModelState.IsValid)
            {
                return View(adModel);
            }
            await adService.CreateAdAsync(adModel);
            return RedirectToAction("AddImages", "AdImage", new { id = adModel.Id });
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
