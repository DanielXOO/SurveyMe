using Answers.Api.Models.Questions;

namespace Answers.Api.Models.Request.Answers;

public abstract class BaseAnswerRequestModel
{
    public QuestionType QuestionType { get; set; }
    
    public Guid QuestionId { get; set; }
}