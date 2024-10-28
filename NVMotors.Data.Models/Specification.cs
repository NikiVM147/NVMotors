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
        public int HorsePower { get; set; }
        [Required]
        public string TransmissionType { get; set; } = String.Empty;
        public FuelType FuelType { get; set; }
        public Color Color { get; set; }
        public Condition Condition { get; set; }
        public Vechicle Vechicle { get; set; }

    }
    


}
