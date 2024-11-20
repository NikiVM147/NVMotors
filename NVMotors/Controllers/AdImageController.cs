using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NVMotors.Data;
using NVMotors.Data.Models;
using NVMotors.Web.ViewModels.Ad;
using NVMotors.Web.ViewModels.AdImage;

namespace NVMotors.Web.Controllers
{
    public class AdImageController : Controller
    {
        private readonly NVMotorsDbContext context;
        public AdImageController(NVMotorsDbContext _context)
        {
            context = _context;
        }
        [HttpGet]
        public IActionResult AddImages(Guid id)
        {
            var model = new CreateAdImagesViewModel();
            model.AdId = id;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddImages(CreateAdImagesViewModel imageModel)
        {
            if (!ModelState.IsValid)
            {
                return View(imageModel);
            }

            var ad = await context.Ads.FindAsync(imageModel.AdId);
            if (ad == null)
            {
                return NotFound();
            }

            foreach (var image in imageModel.Images)
            {
                if (image.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", image.FileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }

                    var motorImage = new MotorImage
                    {
                        ImageUrl = "/images/" + image.FileName
                    };
                    context.MotorImages.Add(motorImage);
                    await context.SaveChangesAsync();

                    var adImage = new AdImage
                    {
                        AdId = ad.Id,
                        ImageId = motorImage.Id
                    };

                    context.AdsImages.Add(adImage);
                }
            }

            await context.SaveChangesAsync();

            return RedirectToAction("Details", "Ad", new { id = imageModel.AdId });
        }
    }
}
