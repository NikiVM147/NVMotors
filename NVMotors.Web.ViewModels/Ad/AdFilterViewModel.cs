using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVMotors.Web.ViewModels.Ad
{
    public class AdFilterViewModel
    {
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public int? MinHorsePower { get; set; }
        public int? MaxHorsePower { get; set; }
        public int? MinEngineDisplacement { get; set; }
        public int? MaxEngineDisplacement { get; set; }
        public string? TransmissionType { get; set; }
        public string? FuelType { get; set; }
        public string? Color { get; set; }
        public string? Condition { get; set; }
        public string? Town { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
