using NVMotors.Web.ViewModels.Ad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVMotors.Sevices.Data.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<AdIndexViewModel>> IndexGetAllAdsToBeApproved();
        Task ApproveAdAsync(Guid id);
    }
}
