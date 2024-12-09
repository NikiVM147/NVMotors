using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace NVMotors.Web.ViewModels.Query
{
    public class QueriesReceivedViewModel
    {
        public Guid AdId { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Description { get; set; }
        public required string Email { get; set; }
        public required string DateRequested { get; set; }
        public required string Make { get; set; }
        public required string Model { get; set; }
        public required string FullName { get; set; }

    }
}
