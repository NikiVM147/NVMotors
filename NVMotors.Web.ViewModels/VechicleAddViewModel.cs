using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic.FileIO;
using NVMotors.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using static NVMotors.Common.Constants;
namespace NVMotors.Web.ViewModels
{
    public class VechicleAddViewModel
    {
        [Required]
        [MaxLength(MakeMaxLength)]
        [MinLength(MakeMinLenght)]
        public string Make { get; set; } = string.Empty;
        [Required]
        [MaxLength(ModelMaxLenght)]
        [MinLength(ModelMinLenght)]
        public string Model { get; set; } = string.Empty;
        [Required]
        public int Year { get; set; }
        [Required]
        public int HorsePower { get; set; }
        public TransmissionType SelectedTransmissionType { get; set; }
        public IEnumerable<SelectListItem>? TransmissionTypes { get; set; }
        public FuelType SelectedFuelType { get; set; }
        public IEnumerable<SelectListItem>? FuelTypes { get; set; }
        public VechicleColor SelectedColor { get; set; }
        public IEnumerable<SelectListItem>? Colors { get; set; }
        public Condition SelectedCondition { get; set; }
        public IEnumerable<SelectListItem>? Conditions { get; set; }
    }
}
