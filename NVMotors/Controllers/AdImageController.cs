using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NVMotors.Data;
using NVMotors.Data.Models;
using NVMotors.Sevices.Data.Interfaces;
using NVMotors.Web.ViewModels.Ad;
using NVMotors.Web.ViewModels.AdImage;

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
            if (!ModelState.IsValid)
            {
                return View(imageModel);
            }

            await adImageService.AddImagesAsync(imageModel);

            return RedirectToAction("Index", "Ad");
        }
    }
}
