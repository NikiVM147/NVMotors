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
        private readonly IAdService adService;
        
        public AdminService(NVMotorsDbContext _context, IAdService _adService)
        {
            context = _context;
            adService = _adService;
        }

        public async Task<IEnumerable<AdIndexViewModel>> IndexGetAllAdsToBeApproved()
        {
            var ads = context.Ads.Where(a => a.IsApproved == false).AsQueryable();
            return adService.AllAdsToModel(ads);
        }

        public async Task ApproveAdAsync(Guid id)
        {
            var ad = await context.Ads.FirstOrDefaultAsync(a => a.Id == id);
            ad.IsApproved = true;
            await context.SaveChangesAsync();
        }
    }
}
