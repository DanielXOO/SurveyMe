using Answers.Models.Questions;

namespace Answers.Models.Answers;

public abstract class BaseQuestionAnswer
{
    public Guid Id { get; set; }

    public QuestionType QuestionType { get; set; }

    public Guid QuestionId { get; set; }
    
    public SurveyAnswer SurveyAnswer { get; set; }
    
    public Guid SurveyAnswerId { get; set; }
}