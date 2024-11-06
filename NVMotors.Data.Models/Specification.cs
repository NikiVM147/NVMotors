using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NVMotors.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NVMotors.Common.Constants;

namespace NVMotors.Data.Models
{
    public class Specification
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public int Year { get; set; }
        [Required]
        [Range(MinHorsePower, MaxHorsePower)]
        public int HorsePower { get; set; }
        [Required]
        [Range(MinEngineDisplacement, MaxEngineDisplacement)]
        public int EngineDisplacement { get; set; }
        [Required]
        [MinLength(MinTransmissionTypeLength)]
        [MaxLength(MaxTransmissionTypeLength)]
        public string TransmissionType { get; set; } = string.Empty;
        [Required]
        [MinLength(MinFuelTypeLength)]
        [MaxLength(MaxFuelTypeLength)]
        public string FuelType { get; set; } = string.Empty;
        [Required]
        [MinLength(MinColorLength)]
        [MaxLength(MaxColorLength)]
        public string Color { get; set; } = string.Empty;
        [Required]
        [MinLength(MinConditionLength)]
        [MaxLength(MaxConditionLength)]
        public string Condition { get; set; } = string.Empty;
        public Motor Vechicle { get; set; }

    }
    


}
