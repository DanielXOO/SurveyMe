namespace SurveyMe.DomainModels.Response.Users;

public sealed class UserWithSurveysCountResponseModel
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string DisplayName { get; set; }

    public DateTime CreationTime { get; set; }

    public string[] RoleNames { get; set; }
    
    public int SurveysCount { get; set; }
}