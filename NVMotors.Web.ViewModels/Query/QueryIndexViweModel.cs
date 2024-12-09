using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVMotors.Web.ViewModels.Query
{
    public class QueryIndexViweModel
    {
        public Guid AdId { get; set; }
        public required string Make { get; set; }
        public required string Model { get; set; }
        public required string Description { get; set; } 
        public required string Date { get; set; }
    }
}
