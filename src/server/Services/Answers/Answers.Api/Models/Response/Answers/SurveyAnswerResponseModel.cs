namespace Answers.Api.Models.Response.Answers;

public sealed class SurveyAnswerResponseModel
{
    public Guid SurveyId { get; set; }
    
    public Guid UserId { get; set; }
        
    public ICollection<BaseAnswerResponseModel> QuestionsAnswers { get; set; }
}