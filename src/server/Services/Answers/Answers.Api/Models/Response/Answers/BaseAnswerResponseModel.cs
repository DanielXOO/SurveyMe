using Answers.Models.Questions;

namespace Answers.Api.Models.Response.Answers;

public abstract class BaseAnswerResponseModel
{
    public QuestionType QuestionType { get; set; }
    
    public Guid QuestionId { get; set; }
}