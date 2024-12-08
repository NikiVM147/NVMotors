using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Rendering;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NVMotors.Sevices.Data
{
    public class AdService : IAdService
    {
        private readonly NVMotorsDbContext context;
        public AdService(NVMotorsDbContext _context)
        {
            context = _context;
        }

        public async Task<Guid> CreateAdAsync(CreateAdViewModel adModel)
        {
            var ad = new Ad
            {
                DateAd = DateTime.Now,
                Description = adModel.Description,
                Price = adModel.Price,
                Town = adModel.Town,
                PhoneNumber = adModel.PhoneNumber,
                MotorId = adModel.MotorModelId,
            };
           await context.Ads.AddAsync(ad);
           await context.SaveChangesAsync();
           return ad.Id;
        }

        public async Task<AdDetailViewModel> GetAdDetailsAsync(Guid id, Guid userId)
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
                IsSeller = userId == ad.Motor.SellerId,
                MotorId = ad.MotorId,
            };
            return model;
        }
     
        public async Task<AdViewModel> IndexGetAllAds(AdFilterViewModel filters, string searchQuery)
        {
            var transmissionTypes = await context.Ads
                .Select(ad => ad.Motor.Specification.TransmissionType)
                .Distinct()
                .ToListAsync();

            var fuelTypes = await context.Ads
                .Select(ad => ad.Motor.Specification.FuelType)
                .Distinct()
                .ToListAsync();

            var colors = await context.Ads
                .Select(ad => ad.Motor.Specification.Color)
                .Distinct()
                .ToListAsync();

            var conditions = await context.Ads
                .Select(ad => ad.Motor.Specification.Condition)
                .Distinct()
                .ToListAsync();

            var towns = await context.Ads
                .Select(ad => ad.Town)
                .Distinct()
                .ToListAsync();

            var adsQuery = context.Ads
                .Include(a => a.Motor)
                .ThenInclude(m => m.Specification)
                .Include(a => a.AdsImages)
                .ThenInclude(ai => ai.Image)
                .Where(a => a.IsApproved)
                .AsQueryable();

            adsQuery = ApplyFilters(adsQuery, filters);

            if (!string.IsNullOrEmpty(searchQuery))
            {
                adsQuery = adsQuery.Where(a => (a.Motor.Make.ToLower() + a.Motor.Model.ToLower()).Contains(searchQuery.ToLower()));


            }

            var filteredAds = await adsQuery.ToListAsync();

            var ads = filteredAds.Select(a => new AdIndexViewModel
            {
                Id = a.Id,
                Make = a.Motor.Make,
                Model = a.Motor.Model,
                Year = a.Motor.Specification.Year,
                Town = a.Town,
                Price = a.Price,
                ImageURL = a.AdsImages.Select(ai => ai.Image.ImageUrl).FirstOrDefault() ?? string.Empty
            }).ToList();

            var viewModel = new AdViewModel
            {
                Ads = ads,
                FilterModel = filters,
                TransmissionTypes = transmissionTypes.Select(t => new SelectListItem { Value = t, Text = t }).ToList(),
                FuelTypes = fuelTypes.Select(t => new SelectListItem { Value = t, Text = t }).ToList(),
                Colors = colors.Select(t => new SelectListItem { Value = t, Text = t }).ToList(),
                Conditions = conditions.Select(t => new SelectListItem { Value = t, Text = t }).ToList(),
                Towns = towns.Select(t => new SelectListItem { Value = t, Text = t }).ToList()
            };

            return viewModel;
        }

        private IQueryable<Ad> ApplyFilters(IQueryable<Ad> query, AdFilterViewModel filters)
        {
            if (filters.MinYear.HasValue)
                query = query.Where(a => a.Motor.Specification.Year >= filters.MinYear.Value);
            if (filters.MaxYear.HasValue)
                query = query.Where(a => a.Motor.Specification.Year <= filters.MaxYear.Value);
            if (filters.MinHorsePower.HasValue)
                query = query.Where(a => a.Motor.Specification.HorsePower >= filters.MinHorsePower.Value);
            if (filters.MaxHorsePower.HasValue)
                query = query.Where(a => a.Motor.Specification.HorsePower <= filters.MaxHorsePower.Value);
            if (filters.MinEngineDisplacement.HasValue)
                query = query.Where(a => a.Motor.Specification.EngineDisplacement >= filters.MinEngineDisplacement.Value);
            if (filters.MaxEngineDisplacement.HasValue)
                query = query.Where(a => a.Motor.Specification.EngineDisplacement <= filters.MaxEngineDisplacement.Value);
            if (!string.IsNullOrEmpty(filters.Town))
                query = query.Where(a => a.Town.Contains(filters.Town));
            if (filters.MinPrice.HasValue)
                query = query.Where(a => a.Price >= filters.MinPrice.Value);
            if (filters.MaxPrice.HasValue)
                query = query.Where(a => a.Price <= filters.MaxPrice.Value);
            if (!string.IsNullOrEmpty(filters.TransmissionType))
                query = query.Where(a => a.Motor.Specification.TransmissionType.Contains(filters.TransmissionType));
            if (!string.IsNullOrEmpty(filters.FuelType))
                query = query.Where(a => a.Motor.Specification.FuelType.Contains(filters.FuelType));
            if (!string.IsNullOrEmpty(filters.Color))
                query = query.Where(a => a.Motor.Specification.Color.Contains(filters.Color));
            if (!string.IsNullOrEmpty(filters.Condition))
                query = query.Where(a => a.Motor.Specification.Condition.Contains(filters.Condition));

            return query;
        }

        public IEnumerable<AdIndexViewModel> AllAdsToModel(IQueryable<Ad> ads) 
        {
            return ads.Select(a => new AdIndexViewModel
            {
                Id = a.Id,
                Make = a.Motor.Make,
                Model = a.Motor.Model,
                Year = a.Motor.Specification.Year,
                Town = a.Town,
                Price = a.Price,
                ImageURL = a.AdsImages.Select(ai => ai.Image.ImageUrl).FirstOrDefault()!,

            });
        }
    }
}
