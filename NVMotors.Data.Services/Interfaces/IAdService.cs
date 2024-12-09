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
        Task<AdViewModel> IndexGetAllAds(AdFilterViewModel filters, string searchQuery, int page, int pageSize);
        Task<Guid> CreateAdAsync(CreateAdViewModel adModel);
        Task<AdDetailViewModel> GetAdDetailsAsync(Guid id, Guid userId);
        Task DeleteAdAsync(Guid id);
        Task<CreateAdViewModel> GetEditViewModelAsync(Guid id, Guid motorId);
        Task<Guid> EditAdAsync(CreateAdViewModel editModel);
        Task<bool> ValidateMotorId(Guid id);


    }
}
