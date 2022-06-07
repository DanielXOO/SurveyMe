using Surveys.Api.Models.Questions;

namespace Surveys.Api.Models.Options;

public sealed class QuestionOption
{
    public Guid Id { get; set; }

    public string Text { get; set; }

    public Question Question { get; set; }

    public Guid QuestionId { get; set; }
}