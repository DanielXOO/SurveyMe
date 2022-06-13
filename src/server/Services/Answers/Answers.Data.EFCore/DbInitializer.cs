using Microsoft.EntityFrameworkCore;

namespace Answers.Data;

public static class DbInitializer
{
    public static async Task Initialize(AnswersDbContext context)
    {
        await context.Database.MigrateAsync();
    }
}