using Authentication.Roles;
using Authentication.Users;
using Microsoft.AspNetCore.Identity;
using AuthenticationDbContext = Authentication.Data.AuthenticationDbContext;
using DbInitializer = Authentication.Data.DbInitializer;
using ILogger = Authentication.Logging.Abstracts.ILogger;

namespace Authentication.Api.Extensions;

public static class ServiceProviderExtension
{
    public static async Task CreateDbIfNotExists(this IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger>();
            try
            {
                var context = services.GetRequiredService<AuthenticationDbContext>();
                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<Role>>();

                await DbInitializer.Initialize(context, userManager, roleManager);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Db do not created");
                throw;
            }
        }
    }
}