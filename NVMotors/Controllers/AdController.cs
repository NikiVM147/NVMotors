using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using NVMotors.Data;
using NVMotors.Data.Models;
using NVMotors.Sevices.Data.Interfaces;
using NVMotors.Web.ViewModels.Ad;
using System.Runtime.ExceptionServices;
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
        public async Task<IActionResult> IndexAds(AdFilterViewModel filters)
        {
            var model = await adService.IndexGetAllAds(filters);
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult CreateAd(Guid id)
        {
            ViewData["Text"] = "Create Ad";
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
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id, Guid motorId)
        {
            ViewData["Text"] = "Edit Ad";
            var ad = await context.Ads
               .Include(a => a.AdsImages) 
               .ThenInclude(ai => ai.Image)
               .FirstOrDefaultAsync(a => a.Id == id);
            var model = new CreateAdViewModel()
            {
                Id = id,
                MotorModelId = motorId,
                DateAd = ad.DateAd,
                Description = ad.Description,
                Price = ad.Price,
                Town = ad.Town,
                PhoneNumber = ad.PhoneNumber,
            };
            return View("CreateAd", model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CreateAdViewModel editModel)
        {
            var ad = await context.Ads.FirstOrDefaultAsync(a => a.Id == editModel.Id);
            if (!ModelState.IsValid) 
            {
                return View("CreateAd", editModel);
            }
            if (ad != null) 
            {
                ad.Description = editModel.Description;
                ad.Price = editModel.Price;
                ad.Town = editModel.Town;
                ad.PhoneNumber = editModel.PhoneNumber;
                ad.IsApproved = false;
            }
            await context.SaveChangesAsync();
            return RedirectToAction("ManageImages", "AdImage", new { id = ad.Id });


        }

        }
}
