using Microsoft.EntityFrameworkCore;
using NVMotors.Data;
using NVMotors.Sevices.Data.Interfaces;
using NVMotors.Web.ViewModels.Ad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVMotors.Sevices.Data
{
    public class AdminService : IAdminService
    {
        private readonly NVMotorsDbContext context;
        
        public AdminService(NVMotorsDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<AdIndexViewModel>> IndexGetAllAdsToBeApproved()
        {
            var ads = context.Ads.Where(a => a.IsApproved == false).AsQueryable();
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

        public async Task ApproveAdAsync(Guid id)
        {
            var ad = await context.Ads.FirstOrDefaultAsync(a => a.Id == id);
            ad.IsApproved = true;
            await context.SaveChangesAsync();
        }
    }
}
