namespace Answers.Models.Answers;

public sealed class CheckboxQuestionAnswer : BaseQuestionAnswer
{
    public ICollection<OptionQuestionAnswer> Options { get; set; }
}