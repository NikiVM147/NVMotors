using Microsoft.EntityFrameworkCore;
using NVMotors.Data;
using NVMotors.Data.Models;
using NVMotors.Sevices.Data.Interfaces;
using NVMotors.Web.ViewModels.Ad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVMotors.Sevices.Data
{
    public class AdService : IAdService
    {
        private readonly NVMotorsDbContext context;
        public AdService(NVMotorsDbContext _context)
        {
            context = _context;
        }

        public async Task CreateAdAsync(CreateAdViewModel adModel)
        {
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
        }

        public async Task<AdDetailViewModel> 0(Guid id)
        {
            var ad = await context.Ads.Include(a => a.Motor)
                .ThenInclude(m => m.MotorCategory)
                .Include(a => a.Motor).ThenInclude(m => m.Specification)
                .Include(a => a.AdsImages)
                .ThenInclude(ai => ai.Image)
                .FirstOrDefaultAsync(a => a.Id == id);
            var model = new AdDetailViewModel
            {
                Id = id,
                Category = ad.Motor.MotorCategory!.Name,
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
            return model;
        }

        public async Task<IEnumerable<AdIndexViewModel>> IndexGetAllAds()
        {
            return context.Ads.Where(a => a.IsApproved == true).Select(a => new AdIndexViewModel
            {
                Id = a.Id,
                Make = a.Motor.Make,
                Model = a.Motor.Model,
                Year = a.Motor.Specification.Year,
                Town = a.Town,
                Price = a.Price,
                ImageURL = a.AdsImages.Select(ai => ai.Image.ImageUrl).FirstOrDefault(),

            });
        }
    }
}
