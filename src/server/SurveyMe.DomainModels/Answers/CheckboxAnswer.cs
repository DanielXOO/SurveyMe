namespace SurveyMe.DomainModels.Answers;

public sealed class CheckboxAnswer : BaseAnswer
{
    public ICollection<OptionAnswer> Options { get; set; }
}