using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVMotors.Web.ViewModels.Ad
{
    public class AdIndexViewModel
    {
        public Guid Id { get; set; }
        public required string Make { get; set; }
        public required string Model { get; set; }
        public int Year { get; set; }
        public required string Town { get; set; }
        public decimal Price { get; set; }
        public required string ImageURL { get; set; }
    }
}
