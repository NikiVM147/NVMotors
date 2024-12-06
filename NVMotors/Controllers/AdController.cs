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
        public async Task<IActionResult> Index()
        {
            var model = await adService.IndexGetAllAds();
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult CreateAd(Guid id)
        {
            var model = new CreateAdViewModel();
            model.MotorModelId = id;
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
            var adId = await adService.CreateAdAsync(adModel);
            return RedirectToAction("AddImages", "AdImage", new { id = adId });
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {

            var model = await adService.GetAdDetailsAsync(id);
            return View(model);
        }

    }
}
