using Answers.Models.Questions;

namespace Answers.Models.Options;

public sealed class Option
{
    public Guid Id { get; set; }
    
    public string Text { get; set; }

    public Question Question { get; set; }

    public Guid QuestionId { get; set; }
}