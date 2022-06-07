using Microsoft.EntityFrameworkCore;

namespace Surveys.Api.Data;

public static class DbInitializer
{
    public static async Task Initialize(SurveysDbContext context)
    {
        await context.Database.MigrateAsync();
    }
}