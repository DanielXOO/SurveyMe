using SurveyMe.WebApplication.Models.Requests.Answers;

namespace SurveyMe.WebApplication.Models.Requests.Surveys;

public sealed class SurveyAnswerRequestModel
{
    public Guid SurveyId { get; set; }
        
    public ICollection<BaseAnswerRequestModel> Questions { get; set; }
}