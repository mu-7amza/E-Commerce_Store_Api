using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Domain.Contracts;
using E_Commerce_Domain.Entities.Identity;
using E_Commerce_Infrastructure.Date;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace E_Commerce_Infrastructure
{
    public class IdentityDataSeeder : IDataSeeder
    {
        private readonly StoreIdentityDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityDataSeeder> _logger;

        public IdentityDataSeeder(StoreIdentityDbContext context, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, ILogger<IdentityDataSeeder> logger)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await _context.Database.MigrateAsync(ct);
            }

            if (!await _roleManager.Roles.AnyAsync())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }

            if (!await _userManager.Users.AnyAsync())
            {
                var user = new ApplicationUser
                {
                    Email = "hamza@gmail.com",
                    UserName = "hamza",
                    DisplayName = "Muhammed Hamza",
                    PhoneNumber = "01234567890",
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd");
                if (!result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "SuperAdmin");
                }
                else
                {
                   _logger.LogError("Failed to create user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}

    

    

