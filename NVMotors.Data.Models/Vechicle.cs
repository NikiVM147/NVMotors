using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NVMotors.Common.Constants;

namespace NVMotors.Data.Models
{
    public class Vechicle
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [MaxLength(MakeMaxLength)]
        [MinLength(MakeMinLenght)]
        public string Make { get; set; } = string.Empty;
        [Required]
        [MaxLength(ModelMaxLenght)]
        [MinLength(ModelMinLenght)]
        public string Model { get; set; } = string.Empty;
        [Required]
        [ForeignKey(nameof(SpecificationId))]
        public Guid SpecificationId { get; set; }
        public Specification Specification { get; set; }
        public ICollection<Ad> Ads { get; set; } = new List<Ad>();

    }
}
