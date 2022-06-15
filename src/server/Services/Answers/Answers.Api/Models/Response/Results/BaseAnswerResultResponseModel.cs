using Answers.Models.Questions;

namespace Answers.Api.Models.Response.Results;

public class BaseAnswerResultResponseModel
{
    public string Title { get; set; }

    public QuestionType QuestionType { get; set; }
}