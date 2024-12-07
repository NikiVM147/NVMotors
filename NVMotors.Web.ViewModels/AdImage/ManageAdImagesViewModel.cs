using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVMotors.Web.ViewModels.AdImage
{
    public class ManageAdImagesViewModel
    {
        public Guid AdId { get; set; }
        public List<ImageViewModel> ExistingImages { get; set; }
    }
}
