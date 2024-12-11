using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NVMotors.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVMotors.Data
{
    public class SeedUsers
    {
        public static async Task SeedRolesAndUsers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            var users = new List<AppUser>{
                new AppUser
                {
                    Id = Guid.Parse("06808eb6-53c1-40b1-8802-40a511d6a737"),
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    FirstName = "admin",
                    LastName = "admin",
                },
                new AppUser
                {
                    Id = Guid.Parse("e6d7e2cf-f056-43b2-bdb0-97b817006d74"),
                    UserName = "seller@gmail.com",
                    Email = "seller@gmail.com",
                    FirstName = "Seller",
                    LastName = "Seller",
                },
                new AppUser
                {
                    Id = Guid.Parse("0a865bea-1234-43ae-ad43-84633ee9d6df"),
                    UserName = "buyer@gmail.com",
                    Email = "buyer@gmail.com",
                    FirstName = "Buyer",
                    LastName = "Buyer",
                } };
            foreach (var user in users) 
            {
                var exists = await userManager.FindByIdAsync(user.Id.ToString());
                if (exists == null)
                {
                    var result = await userManager.CreateAsync(user, "123456");
                    if (result.Succeeded)
                    {
                        var role = user.Email == "admin@gmail.com" ? "Administrator" : "User";
                        await userManager.AddToRoleAsync(user, role);
                    }
                }
            }

        }
    }
}
