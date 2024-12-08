using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVMotors.Web.ViewModels.Ad
{
    public class AdViewModel
    {
        public IEnumerable<AdIndexViewModel> Ads { get; set; }
        public AdFilterViewModel FilterModel { get; set; }

        public List<SelectListItem> TransmissionTypes { get; set; }
        public List<SelectListItem> FuelTypes { get; set; }
        public List<SelectListItem> Colors { get; set; }
        public List<SelectListItem> Conditions { get; set; }
        public List<SelectListItem> Towns { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string SearchQuery { get; set; }
    }
}
