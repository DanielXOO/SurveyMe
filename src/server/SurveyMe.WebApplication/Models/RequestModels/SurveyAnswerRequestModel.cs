namespace SurveyMe.WebApplication.Models.RequestModels;

public sealed class SurveyAnswerRequestModel
{
    public Guid SurveyId { get; set; }
        
    public ICollection<QuestionAnswerRequestModel> Questions { get; set; }
}