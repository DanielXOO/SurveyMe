using Answers.Models.Questions;

namespace Answers.Services.Models.Questions;

public class BaseAnswerResult
{
    public string Title { get; set; }
    
    public QuestionType QuestionType { get; set; }
}