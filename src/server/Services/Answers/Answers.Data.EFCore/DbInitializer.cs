namespace Answers.Data;

public static class DbInitializer
{
    public static async Task Initialize(AnswersDbContext context)
    {
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
    }
}