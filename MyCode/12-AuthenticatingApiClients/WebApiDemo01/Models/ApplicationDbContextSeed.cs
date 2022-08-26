// Copyright (c) xxx, 2022. All rights reserved.

using Microsoft.AspNetCore.Identity;

namespace WebApiDemo01.Models;

public class ApplicationDbContextSeed
{
    public static async Task SeedData(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var currentServices = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("SeedData");
        try
        {
            var userManager = currentServices.GetRequiredService<UserManager<User>>();
            var roleManager = currentServices.GetRequiredService<RoleManager<IdentityRole>>();
            if (userManager.Users.Any())
            {
                await Task.CompletedTask;
                logger.LogWarning("No need to seed data");
                return;
            }
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Administrator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.User.ToString()));

            //Seed Default User
            var defaultUser = new User { UserName = Authorization.default_username, Email = Authorization.default_email, EmailConfirmed = true, PhoneNumberConfirmed = true };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, Authorization.default_password);
                await userManager.AddToRoleAsync(defaultUser, Authorization.default_role.ToString());
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error to seed database");
        }
    }
}
