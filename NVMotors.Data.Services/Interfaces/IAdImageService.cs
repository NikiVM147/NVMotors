using Microsoft.AspNetCore.Mvc;
using NVMotors.Web.ViewModels.AdImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVMotors.Sevices.Data.Interfaces
{
    public interface IAdImageService
    {
        Task AddImagesAsync(CreateAdImagesViewModel imageModel);
    }
}
