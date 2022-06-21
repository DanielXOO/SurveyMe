using Surveys.Api.Models.Request.Options;
using Surveys.Models.Questions;

namespace Surveys.Api.Models.Request.Questions;

public sealed class QuestionEditRequestModel
{
    public string Title { get; set; }
        
    public QuestionType Type { get; set; }

    public ICollection<QuestionOptionEditRequestModel>? Options { get; set; }
}