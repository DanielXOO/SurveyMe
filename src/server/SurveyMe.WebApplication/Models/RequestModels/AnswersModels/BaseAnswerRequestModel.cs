using SurveyMe.DomainModels;

namespace SurveyMe.WebApplication.Models.RequestModels.AnswersModels;

public abstract class BaseAnswerRequestModel
{
    public QuestionType QuestionType { get; set; }
    public Guid QuestionId { get; set; }
}