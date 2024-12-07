using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NVMotors.Data;
using NVMotors.Data.Models;
using NVMotors.Sevices.Data.Interfaces;
using NVMotors.Web.ViewModels.Ad;
using NVMotors.Web.ViewModels.AdImage;
using static NVMotors.Common.Constants;

namespace NVMotors.Web.Controllers
{
    [Authorize(Roles = "User")]
    public class AdImageController : Controller
    {
        private readonly NVMotorsDbContext context;
        private readonly IAdImageService adImageService;
        public AdImageController(NVMotorsDbContext _context, IAdImageService _adImageService)
        {
            context = _context;
            adImageService = _adImageService;
        }
        [HttpGet]
        public async Task<IActionResult> AddImages(Guid id)
        {
            var model = new CreateAdImagesViewModel
            {
                AdId = id, 
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddImages(CreateAdImagesViewModel imageModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(imageModel);
                }

                await adImageService.AddImagesAsync(imageModel);
                TempData[nameof(Success)] = "Successfull! Pending approval by Admin";
                return RedirectToAction("IndexAds", "Ad");
            }
            catch (Exception ex) when (ex is ArgumentNullException)
            {
                TempData[nameof(Error)] = ex.Message;
                return RedirectToAction("Index", "Motor");
            }
           
        }
        [HttpGet]
        public async Task<IActionResult> ManageImages(Guid id)
        {
            var ad = await context.Ads
                .Include(a => a.AdsImages).ThenInclude(ai => ai.Image)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (ad == null)
            {
                return NotFound();
            }

            var model = new ManageAdImagesViewModel
            {
                AdId = ad.Id,
                ExistingImages = ad.AdsImages.Select(i => new ImageViewModel
                {
                    Id = i.Image.Id,
                    Url = i.Image.ImageUrl,
                }).ToList()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteImage(Guid imageId, Guid adId)
        {
            var adImage = await context.AdsImages
                .Include(ai => ai.Image)
                .FirstOrDefaultAsync(ai => ai.AdId == adId && ai.ImageId == imageId);

            if (adImage == null)
            {
                return NotFound();
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", adImage.Image.ImageUrl.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            context.AdsImages.Remove(adImage);
            context.MotorImages.Remove(adImage.Image);
            await context.SaveChangesAsync();

            TempData["Success"] = "Image deleted successfully.";
            return RedirectToAction(nameof(ManageImages), new { id = adId });
        }
        [HttpPost]
        public IActionResult SetSuccessMessage()
        {
            TempData[nameof(Success)] = "Successfully created Ad. Pending approval by Admin";
            return RedirectToAction("IndexAds","Ad"); // Redirect as needed
        }

    }
}
