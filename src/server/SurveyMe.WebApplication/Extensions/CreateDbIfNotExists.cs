using Microsoft.AspNetCore.Identity;
using SurveyMe.Common.Time;
using SurveyMe.Data;
using SurveyMe.DomainModels;
using ILogger = SurveyMe.Common.Logging.Abstracts.ILogger;

namespace SurveyMe.WebApplication;

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
                var context = services.GetRequiredService<SurveyMeDbContext>();
                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<Role>>();
                var systemClock = services.GetService<ISystemClock>();

                await DbInitializer.Initialize(context, userManager, roleManager, systemClock);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Db do not created");
                throw;
            }
        }
    }
}