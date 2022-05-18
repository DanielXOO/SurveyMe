using SurveyMe.DomainModels.Common;

namespace SurveyMe.DomainModels.Request.Answers;

public abstract class BaseAnswerRequestModel
{
    public QuestionType QuestionType { get; set; }
    
    public Guid QuestionId { get; set; }
}