using SurveyMe.DomainModels.Questions;

namespace SurveyMe.WebApplication.Models.Requests.Answers;

public abstract class BaseAnswerRequestModel
{
    public QuestionType QuestionType { get; set; }
    
    public Guid QuestionId { get; set; }
}