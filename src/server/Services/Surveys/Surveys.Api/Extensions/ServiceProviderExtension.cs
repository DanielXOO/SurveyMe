using Surveys.Data;

namespace Surveys.Api.Extensions;

public static class ServiceProviderExtension
{
    public static async Task CreateDbIfNotExists(this IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<SurveysDbContext>();

            await DbInitializer.Initialize(context);
        }
    }
}