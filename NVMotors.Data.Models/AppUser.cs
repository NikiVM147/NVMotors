using Microsoft.AspNetCore.Identity;

namespace NVMotors.Data.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public AppUser()
        {
            this.Id = Guid.NewGuid();
        }
        public string FirstName { get; set; }    

    }
}
