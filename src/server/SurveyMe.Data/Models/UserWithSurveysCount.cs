using SurveyMe.DomainModels;

namespace SurveyMe.Data.Models;

public sealed class UserWithSurveysCount
{
    public User User { get; set; }

    public int SurveysCount { get; set; }
}