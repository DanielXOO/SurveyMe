namespace SurveyMe.DomainModels.Request.Answers;

public sealed class SurveyAnswerRequestModel
{
    public Guid Id { get; set; }
    
    public Guid SurveyId { get; set; }
        
    public ICollection<BaseAnswerRequestModel> QuestionsAnswers { get; set; }
}