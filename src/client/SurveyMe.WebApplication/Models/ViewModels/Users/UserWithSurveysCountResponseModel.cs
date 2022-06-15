namespace SurveyMe.WebApplication.Models.ViewModels.Users;

public sealed class UserWithSurveysCountViewModel
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string DisplayName { get; set; }

    public DateTime CreationTime { get; set; }

    public string[] RoleNames { get; set; }
    
    public int SurveysCount { get; set; }
}