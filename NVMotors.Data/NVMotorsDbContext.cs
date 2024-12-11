using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NVMotors.Data.Models;
using NVMotors.Data.Models.Enums;
using System.Reflection.Emit;

namespace NVMotors.Data
{
    public class NVMotorsDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>,Guid>
    {
        public NVMotorsDbContext(DbContextOptions<NVMotorsDbContext> options)
            : base(options)
        {
        }
        public DbSet<Motor> Motors { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<AdImage> AdsImages { get; set; }
        public DbSet<MotorImage> MotorImages { get; set; }
        public DbSet<MotorCategory> MotorCategories { get; set; }
        public DbSet<Query> Queries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
