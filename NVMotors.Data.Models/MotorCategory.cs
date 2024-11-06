using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVMotors.Data.Models
{
    public class MotorCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Motor> Motors { get; set; } = new List<Motor>();
    }
}
