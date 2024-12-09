using NVMotors.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NVMotors.Common.Constants;

namespace NVMotors.Web.ViewModels.Query
{
    public class MakeQueryViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [RegularExpression(@"^(\+359|0)\d{9}$", ErrorMessage = "Please enter valid phone number")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [MinLength(DescriptionMinLengt)]
        [MaxLength(DescriptionMaxLengt)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [ForeignKey(nameof(AdId))]
        public Guid AdId { get; set; }
        public Guid RequesterId { get; set; }
    }
}
