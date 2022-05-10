using SurveyMe.DomainModels;

namespace SurveyMe.Foundation.Models;

public sealed class UserWithSurveysCount
{
    public User User { get; set; }
    public int SurveysCount { get; set; }
}