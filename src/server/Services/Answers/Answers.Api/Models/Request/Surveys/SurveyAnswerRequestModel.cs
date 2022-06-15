using Answers.Api.Models.Request.Answers;

namespace Answers.Api.Models.Request.Surveys;

public sealed class SurveyAnswerRequestModel
{
    public Guid SurveyId { get; set; }
        
    public ICollection<BaseAnswerRequestModel> QuestionsAnswers { get; set; }
}