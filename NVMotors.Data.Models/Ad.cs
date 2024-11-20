﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NVMotors.Common.Constants;

namespace NVMotors.Data.Models
{
    public class Ad
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DateAd { get; set; }
        [Required]
        [MinLength(DescriptionMinLengt)]
        [MaxLength(DescriptionMaxLengt)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [ForeignKey(nameof(VechicleId))]
        public Guid VechicleId { get; set; }
        public Motor Vechicle { get; set; }
        [Required]
        [Range(MinPrice, MaxPrice)]
        public decimal Price { get; set; }
        public virtual ICollection<AdImage> AdsImages { get; set; } = new List<AdImage>();
        [Required]
        public string Town { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^(\+359|0)\d{9}$", ErrorMessage = "Please enter valid phone number")]
        public string PhoneNumber  { get; set; } = string.Empty;
    }
}
