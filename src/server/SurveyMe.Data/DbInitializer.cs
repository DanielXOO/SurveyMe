﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SurveyMe.Common.Time;
using SurveyMe.DomainModels.Roles;
using SurveyMe.DomainModels.Users;

namespace SurveyMe.Data;

public static class DbInitializer
{
    public static async Task Initialize(SurveyMeDbContext context,
        UserManager<User> userManager, RoleManager<Role> roleManager,
        ISystemClock systemClock)
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
                DisplayName = "User1",
                Password = "User1234",
                RoleNames = new[] {RoleNames.User},
                UserName = "User1"
            },
            new InitUser
            {
                DisplayName = "User2",
                Password = "User1234",
                RoleNames = new[] {RoleNames.User},
                UserName = "User2"
            },
            new InitUser
            {
                DisplayName = "User3",
                Password = "User1234",
                RoleNames = new[] {RoleNames.User},
                UserName = "User4"
            },
            new InitUser
            {
                DisplayName = "User5",
                Password = "User1234",
                RoleNames = new[] {RoleNames.User},
                UserName = "User5"
            },
            new InitUser
            {
                DisplayName = "User6",
                Password = "User1234",
                RoleNames = new[] {RoleNames.User},
                UserName = "User6"
            },
            new InitUser
            {
                DisplayName = "User7",
                Password = "User1234",
                RoleNames = new[] {RoleNames.User},
                UserName = "User7"
            },
            new InitUser
            {
                DisplayName = "User8",
                Password = "User1234",
                RoleNames = new[] {RoleNames.User},
                UserName = "User8"
            },
            new InitUser
            {
                DisplayName = "User9",
                Password = "User1234",
                RoleNames = new[] {RoleNames.User},
                UserName = "User9"
            }
        };

        await AddOrUpdateRolesAsync(users, roleManager);
        await AddOrUpdateUsersAsync(users, userManager, systemClock);
    }


    private static async Task AddOrUpdateUsersAsync(IEnumerable<InitUser> users,
        UserManager<User> userManager, ISystemClock systemClock)
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
                    Roles = new List<Role>(),
                    CreationTime = systemClock.UtcNow
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