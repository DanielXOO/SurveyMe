using SurveyMe.Repositories;
using ILogger = SurveyMe.Common.Logging.ILogger;

namespace SurveyMe.WebApplication;

public static class CreateDbIfNotExistsExtension
{
    public static void CreateDbIfNotExists(this IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger>();
            try
            {
                var context = services.GetRequiredService<SurveyMeDbContext>();
                
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Db do not created");
                throw;
            }
        }
    }
}