namespace SurveyMe.Repositories
{
    public static class DbInitializer
    {
        public static void Initialize(SurveyMeDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}