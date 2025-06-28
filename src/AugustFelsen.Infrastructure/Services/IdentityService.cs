using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AugustFelsen.Domain.Entities;

namespace AugustFelsen.Infrastructure.Services;

public class IdentityService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<IdentityService> _logger;

    public IdentityService(IServiceProvider serviceProvider, ILogger<IdentityService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task SeedRolesAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<UserRole>>();

        var roles = new[] { "Admin", "Client", "Professional", "Artisan", "Supplier" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                var result = await roleManager.CreateAsync(new UserRole { Id = Guid.NewGuid().ToString(), Name = role });
                if (result.Succeeded)
                {
                    _logger.LogInformation($"Role '{role}' created successfully.");
                }
                else
                {
                    _logger.LogError($"Failed to create role '{role}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }

    public async Task SeedAdminUserAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        var adminEmail = "admin@augustfelsen.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                FirstName = "Admin",
                LastName = "User",
                UserType = Domain.Enums.UserType.Professional
            };

            var result = await userManager.CreateAsync(adminUser, "Admin123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
                _logger.LogInformation("Admin user created successfully.");
            }
            else
            {
                _logger.LogError($"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }
} 