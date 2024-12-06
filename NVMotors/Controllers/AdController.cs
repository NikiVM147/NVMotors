using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using NVMotors.Data;
using NVMotors.Data.Models;
using NVMotors.Sevices.Data.Interfaces;
using NVMotors.Web.ViewModels.Ad;
using System.Security.Claims;

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
        public Guid GetCurrentUserId()
        {
            if (Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            {
                return userId;
            }
            return Guid.Empty;
        }
        public async Task<IActionResult> IndexAds()
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

            var model = await adService.GetAdDetailsAsync(id, GetCurrentUserId());
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ad = await context.Ads.FirstOrDefaultAsync(a => a.Id == id);
            if (ad != null) 
            {
                 context.Remove(ad);
                context.SaveChanges();
            }
            return RedirectToAction(nameof(IndexAds));
        }

    }
}
