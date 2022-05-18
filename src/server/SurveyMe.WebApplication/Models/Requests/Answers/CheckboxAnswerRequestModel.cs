namespace SurveyMe.WebApplication.Models.Requests.Answers;

public sealed class CheckboxAnswerRequestModel : BaseAnswerRequestModel
{
    public IEnumerable<Guid>? OptionIds { get; set; }
}