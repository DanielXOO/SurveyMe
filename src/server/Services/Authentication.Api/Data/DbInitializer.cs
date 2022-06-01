using Authentication.Api.Models.Roles;
using Authentication.Api.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Api.Data;

public static class DbInitializer
{
    public static async Task Initialize(AuthenticationDbContext context,
        UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        await context.Database.MigrateAsync();

        var users = new[]
        {
            new InitUser
            {
                DisplayName = "Admin",
                Password = "Admin123",
                RoleNames = new[] {RoleNames.Admin},
                UserName = "Admin"
            },
            new InitUser
            {
                DisplayName = "User",
                Password = "User1234",
                RoleNames = new[] {RoleNames.User},
                UserName = "User"
            }
        };

        await AddOrUpdateRolesAsync(users, roleManager);
        await AddOrUpdateUsersAsync(users, userManager);
    }


    private static async Task AddOrUpdateUsersAsync(IEnumerable<InitUser> users,
        UserManager<User> userManager)
    {
        foreach (var user in users)
        {
            var registeredUser = await userManager.FindByNameAsync(user.UserName);
            if (registeredUser == null)
            {
                var newUser = new User
                {
                    DisplayName = user.DisplayName,
                    UserName = user.UserName,
                    Roles = new List<Role>()
                };
                await userManager.CreateAsync(newUser, user.Password);

                await userManager.AddToRolesAsync(newUser, user.RoleNames);
            }
            else
            {
                var removedRoles = registeredUser.Roles
                    .Select(role => role.Name).Except(user.RoleNames);

                await userManager.RemoveFromRolesAsync(registeredUser, removedRoles);

                var addedRoles = user.RoleNames.Except(registeredUser.Roles.Select(role => role.Name));

                await userManager.AddToRolesAsync(registeredUser, addedRoles);

                await userManager.UpdateAsync(registeredUser);
            }
        }
    }

    private static async Task AddOrUpdateRolesAsync(IEnumerable<InitUser> users, RoleManager<Role> roleManager)
    {
        var rolesNames = users
            .SelectMany(user => user.RoleNames)
            .Distinct()
            .Select(role => role.ToUpper());

        foreach (var roleName in rolesNames)
        {
            var existedRole = await roleManager.FindByNameAsync(roleName);
            if (existedRole == null)
            {
                await roleManager.CreateAsync(new Role {Name = roleName});
            }
            else
            {
                await roleManager.UpdateAsync(existedRole);
            }
        }
    }

    private class InitUser
    {
        public string UserName { get; init; }

        public string DisplayName { get; init; }

        public string Password { get; init; }

        public string[] RoleNames { get; init; }
    }
}