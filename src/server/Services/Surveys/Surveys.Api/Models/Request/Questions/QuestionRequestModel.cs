using Surveys.Models.Questions;

namespace Surveys.Api.Models.Request.Questions;

public sealed class QuestionRequestModel
{
    public string Title { get; set; }
        
    public QuestionType Type { get; set; }

    public ICollection<QuestionOptionRequestModel>? Options { get; set; }
}