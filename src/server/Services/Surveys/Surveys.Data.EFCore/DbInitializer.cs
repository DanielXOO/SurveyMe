using Microsoft.EntityFrameworkCore;

namespace Surveys.Data;

public static class DbInitializer
{
    public static async Task Initialize(SurveysDbContext context)
    {
        await context.Database.MigrateAsync();
    }
}