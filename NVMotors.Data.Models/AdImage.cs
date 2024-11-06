using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVMotors.Data.Models
{
    [PrimaryKey(nameof(ImageId),nameof(AdId))]
    public class AdImage
    {
        [Required]
        [ForeignKey(nameof(ImageId))]
        public Guid ImageId { get; set; }
        public MotorImage Image { get; set; }
        [Required]
        [ForeignKey(nameof(AdId))]
        public Guid AdId { get; set; }
        public Ad Ad { get; set; }
    }
}
