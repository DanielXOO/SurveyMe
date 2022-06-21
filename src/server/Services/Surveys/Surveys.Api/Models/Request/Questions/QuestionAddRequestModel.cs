using Surveys.Api.Models.Request.Options;
using Surveys.Models.Questions;

namespace Surveys.Api.Models.Request.Questions;

public sealed class QuestionAddRequestModel
{
    public string Title { get; set; }
        
    public QuestionType Type { get; set; }

    public ICollection<QuestionOptionAddRequestModel>? Options { get; set; }
}