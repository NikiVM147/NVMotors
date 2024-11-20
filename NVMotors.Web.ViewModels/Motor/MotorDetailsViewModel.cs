using NVMotors.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVMotors.Web.ViewModels.Motor
{
    public class MotorDetailsViewModel
    {
        public Guid Id { get; set; }
        public required string Make { get; set; }
        public required string Model { get; set; }
        public int Year { get; set; }
        public int HorsePower { get; set; }
        public int EngineDisplacement { get; set; }
        public required string TransmissionType { get; set; }
        public required string FuelType { get; set; }
        public required string Color { get; set; }
        public required string Condition { get; set; }
    }
}
