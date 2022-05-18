using SurveyMe.WebApplication.Models.RequestModels.AnswersModels;
using SurveyMe.WebApplication.Models.RequestModels.QuestionModels;

namespace SurveyMe.WebApplication.Models.RequestModels.SurveyModels;

public sealed class SurveyAnswerRequestModel
{
    public Guid SurveyId { get; set; }
        
    public ICollection<BaseAnswerRequestModel> Questions { get; set; }
}