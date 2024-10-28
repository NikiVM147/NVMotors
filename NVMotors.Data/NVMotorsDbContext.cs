using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NVMotors.Data.Models;

namespace NVMotors.Data
{
    public class NVMotorsDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>,Guid>
    {
        public NVMotorsDbContext(DbContextOptions<NVMotorsDbContext> options)
            : base(options)
        {
        }
        public DbSet<Vechicle> Vechicles { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<Ad> Ads { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
