using Microsoft.AspNetCore.Identity;
using NVMotors.Data.Models;
using NVMotors.Web.ViewModels.Ad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVMotors.Sevices.Data.Interfaces
{
    public interface IAdService
    {
        Task<IEnumerable<AdIndexViewModel>> IndexGetAllAds();
        Task<Guid> CreateAdAsync(CreateAdViewModel adModel);
        Task<AdDetailViewModel> GetAdDetailsAsync(Guid id, Guid userId);
        IEnumerable<AdIndexViewModel> AllAdsToModel(IQueryable<Ad> ads);


    }
}
