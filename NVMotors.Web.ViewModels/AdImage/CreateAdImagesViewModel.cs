using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVMotors.Web.ViewModels.AdImage
{
    public class CreateAdImagesViewModel
    {
        public Guid AdId { get; set; }

        [Required]
        public IFormFileCollection Images { get; set; }
    }
}
