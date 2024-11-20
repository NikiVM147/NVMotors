using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NVMotors.Data.Models;
using NVMotors.Web.ViewModels.Motor;

namespace NVMotors.Sevices.Data.Interfaces
{
    public interface IMotorService
    {
        Task<List<MotorIndexViewModel>> GetAllMotorsForCurrentUserAsync(Guid userId);
        List<SelectListItem> GetEnumSelectList<TEnum>() where TEnum : Enum;

        MotorAddViewModel LoadMotorViewModel();
        Task CreateMotorAsync(MotorAddViewModel addModel, Guid userId);
        Task<MotorDetailsViewModel> DetailsMotorAsync(Guid id);

        Task<Motor> FindMotorByIdAsync(Guid id);
        Task<MotorAddViewModel> LoadEditModelAsync(Guid id);
        Task EditMotorAsync(MotorAddViewModel editModel);
    }
}
