using NVMotors.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NVMotors.Common.Constants;

namespace NVMotors.Web.ViewModels.Ad
{
    public class CreateAdViewModel
    {
        public Guid Id { get; set; }
        public Guid MotorModelId { get; set; }
        public DateTime DateAd { get; set; }
        [Required]
        [MinLength(DescriptionMinLengt)]
        [MaxLength(DescriptionMaxLengt)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(MinPrice, MaxPrice)]
        public decimal Price { get; set; }
        [Required]
        public string Town { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^(\+359|0)\d{9}$", ErrorMessage = "Please enter valid phone number")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
