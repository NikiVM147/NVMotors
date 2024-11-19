using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static NVMotors.Common.Constants;

namespace NVMotors.Data.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public AppUser()
        {
            this.Id = Guid.NewGuid();
        }
        [Required]
        [MinLength(MinLenghtName)]
        [MaxLength(MaxLenghtName)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(MinLenghtName)]
        [MaxLength(MaxLenghtName)]
        public string LastName { get; set; }

    }
}
