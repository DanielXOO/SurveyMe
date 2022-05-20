using SurveyMe.DomainModels.Users;

namespace SurveyMe.Foundation.Models.Users;

public sealed class UserWithSurveysCount
{
    public User User { get; set; }
    public int SurveysCount { get; set; }
}