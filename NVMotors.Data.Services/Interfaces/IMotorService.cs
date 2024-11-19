using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NVMotors.Web.ViewModels;

namespace NVMotors.Sevices.Data.Interfaces
{
    public interface IMotorService
    {
        Task<List<MotorIndexViewModel>> GetAllMotorsForCurrentUserAsync(Guid userId);
        List<SelectListItem> GetEnumSelectList<TEnum>() where TEnum : Enum;

        MotorAddViewModel LoadMotorViewModel();
        Task CreateMotorAsync(MotorAddViewModel addModel, Guid userId);
        Task<MotorDetailsViewModel> DetailsMotorAsync(Guid id);
    }
}
