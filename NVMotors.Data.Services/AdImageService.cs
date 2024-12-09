using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using NVMotors.Data;
using NVMotors.Data.Models;
using NVMotors.Sevices.Data.Interfaces;
using NVMotors.Web.ViewModels.AdImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVMotors.Sevices.Data
{
    public class AdImageService : IAdImageService
    {
        private readonly NVMotorsDbContext context;
        public AdImageService(NVMotorsDbContext _context)
        {
            context = _context;
        }
        public async Task AddImagesAsync(CreateAdImagesViewModel imageModel)
        {
            var ad = await context.Ads.FirstOrDefaultAsync(a => a.Id == imageModel.AdId);
            if (ad == null) 
            {
                throw new ArgumentNullException("Ad not found");
            }
            var motorImages = new List<MotorImage>();
            var adImages = new List<AdImage>();
            foreach (var image in imageModel.Images)
            {
                if (image.Length > 0)
                {
                    var uniqueFileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }
                    var motorImage = new MotorImage
                    {
                        ImageUrl = "/images/" + uniqueFileName
                    };
                    motorImages.Add(motorImage);
                }
            }
            context.MotorImages.AddRange(motorImages);
            await context.SaveChangesAsync();
            foreach (var motorImage in motorImages)
            {
                adImages.Add(new AdImage
                {
                    AdId = ad.Id,
                    ImageId = motorImage.Id
                });
            }
            context.AdsImages.AddRange(adImages);
            await context.SaveChangesAsync();
        }

        public async Task DeleteImageAsync(Guid imageId, Guid adId)
        {
            var adImage = await context.AdsImages
                .Include(ai => ai.Image)
                .FirstOrDefaultAsync(ai => ai.AdId == adId && ai.ImageId == imageId);

            if (adImage == null)
            {
                throw new ArgumentNullException("Ad image not found");
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", adImage.Image.ImageUrl.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            context.AdsImages.Remove(adImage);
            context.MotorImages.Remove(adImage.Image);
            await context.SaveChangesAsync();
        }

        public async Task<ManageAdImagesViewModel> ManageImagesAsync(Guid id)
        {
            var ad = await context.Ads
                .Include(a => a.AdsImages).ThenInclude(ai => ai.Image)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (ad == null)
            {
                throw new NullReferenceException("Ad not found!");
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
            return model;
        }
    }
}
