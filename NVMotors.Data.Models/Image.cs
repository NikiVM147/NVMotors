using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVMotors.Data.Models
{
    public class Image
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string ImageUrl { get; set; }  = string.Empty;

        public virtual ICollection<AdImage> AdsImages { get; set; } = new List<AdImage>();


    }
}
