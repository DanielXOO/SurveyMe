namespace Answers.Api.Models.Answers;

public sealed class CheckboxAnswer : BaseQuestionAnswer
{
    public ICollection<OptionAnswer> Options { get; set; }
}