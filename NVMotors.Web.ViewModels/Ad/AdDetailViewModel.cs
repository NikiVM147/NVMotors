using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace NVMotors.Web.ViewModels.Ad
{
    public class AdDetailViewModel
    {
        public Guid Id { get; set; }
        public required string Category { get; set; }
        public required string Make { get; set; }
        public required string Model { get; set; }
        public int Year { get; set; }
        public int HorsePower { get; set; }
        public int EngineDisplacement { get; set; }
        public required string TransmissionType { get; set; }
        public required string FuelType { get; set; }
        public required string Color { get; set; }
        public required string Condition { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public required string Town { get; set; }
        public required string PhoneNumber { get; set; }
        public List<string> ImageURLs { get; set; } = new List<string>();


    }
}
