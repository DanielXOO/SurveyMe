using Answers.Api.Models.Questions;

namespace Answers.Api.Models.Response.Questions;

public sealed class QuestionResponseModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }
        
    public QuestionType Type { get; set; }

    public ICollection<QuestionOptionResponseModel> Options { get; set; }
}