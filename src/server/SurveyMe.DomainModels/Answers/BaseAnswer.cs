using SurveyMe.DomainModels.Questions;

namespace SurveyMe.DomainModels.Answers;

public abstract class BaseAnswer
{
    public Guid Id { get; set; }

    public QuestionType QuestionType { get; set; }

    public Guid QuestionId { get; set; }

    public Question Question { get; set; }
    
    public SurveyAnswer SurveyAnswer { get; set; }
    
    public Guid SurveyAnswerId { get; set; }
}