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

    }
}
