namespace SurveyMe.WebApplication.Models.RequestModels.AnswersModels;

public sealed class CheckboxAnswerRequestModel : BaseAnswerRequestModel
{
    public IEnumerable<Guid>? OptionIds { get; set; }
}