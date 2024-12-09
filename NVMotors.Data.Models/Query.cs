using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NVMotors.Common.Constants;

namespace NVMotors.Data.Models
{
    public class Query
    {
        [Key]
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
        public Ad Ad { get; set; } = null!;
        public Guid RequesterId { get; set; }
        [ForeignKey(nameof(RequesterId))]
        public AppUser Requester { get; set; } = null!;
    }
}
