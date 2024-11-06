using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NVMotors.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
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
        //[MinLength(MinTransmissionTypeLength)]
        //[MaxLength(MaxTransmissionTypeLength)]
        public TransmissionType TransmissionType { get; set; } 
        [Required]
        //[MinLength(MinFuelTypeLength)]
        //[MaxLength(MaxFuelTypeLength)]
        public FuelType FuelType { get; set; } 
        [Required]
        //[MinLength(MinColorLength)]
        //[MaxLength(MaxColorLength)]
        public MotorColor Color { get; set; } 
        [Required]
        //[MinLength(MinConditionLength)]
        //[MaxLength(MaxConditionLength)]
        public Condition Condition { get; set; } 
        public Motor Vechicle { get; set; }

    }
    


}
