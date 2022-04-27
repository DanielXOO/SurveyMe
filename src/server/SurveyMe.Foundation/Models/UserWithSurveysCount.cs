using SurveyMe.DomainModels;

namespace SurveyMe.Surveys.Foundation.Models
{
    public sealed class UserWithSurveysCount
    {
        public User User { get; set; }
        public int SurveysCount { get; set; }
    }
}