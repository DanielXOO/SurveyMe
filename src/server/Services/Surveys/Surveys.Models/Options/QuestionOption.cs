using Surveys.Models.Questions;

namespace Surveys.Models.Options;

public sealed class QuestionOption
{
    public Guid Id { get; set; }

    public string Text { get; set; }

    public Question Question { get; set; }

    public Guid QuestionId { get; set; }
}