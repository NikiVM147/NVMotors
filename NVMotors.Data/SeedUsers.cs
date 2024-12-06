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

            var adminEmail = "admin@gmail.com";
            var adminPassword = "123456";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "admin",
                    LastName = "admin",
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Administrator");
                }
            }
            foreach (var user in userManager.Users.ToList()) 
            {
                if (user.UserName != adminEmail)
                {
                    if (!await userManager.IsInRoleAsync(user, "User")) 
                    {
                        await userManager.AddToRoleAsync(user, "User");
                    }
                }
            }

        }
    }
}
