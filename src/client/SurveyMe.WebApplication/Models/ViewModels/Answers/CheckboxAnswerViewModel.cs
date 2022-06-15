namespace SurveyMe.WebApplication.Models.ViewModels.Answers;

public sealed class CheckboxAnswerViewModel : BaseAnswerViewModel
{
    public IEnumerable<Guid>? OptionIds { get; set; }
}