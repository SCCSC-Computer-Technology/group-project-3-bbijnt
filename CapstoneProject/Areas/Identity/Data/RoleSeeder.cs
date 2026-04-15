using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using CapstoneProject.Areas.Identity.Data;

namespace CapstoneProject.Data
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<CapstoneProjectUser>>();

            // If more roles are needed, please add them here
            string[] roleNames = { "Admin", "Student", "Volunteer", "Staff" };

            foreach (var roleName in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                    Console.WriteLine($"Role '{roleName}' created.");
                }
            }

            // Seed default admin user
            string adminEmail = "admin@scc.edu";
            string adminPassword = "Admin@1234!"; // Change after first login
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new CapstoneProjectUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    StudentId = "0000000",
                    FirstName = "Admin",
                    LastName = "User",
                    RegistrationDate = DateTime.UtcNow
                };
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    Console.WriteLine("Default admin user created.");
                }
            }
            // Assign to Admin role if not already
            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
                Console.WriteLine("Default admin user assigned to Admin role.");
            }
        }
    }
}