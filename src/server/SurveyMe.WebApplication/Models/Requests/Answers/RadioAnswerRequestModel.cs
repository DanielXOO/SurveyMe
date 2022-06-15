namespace SurveyMe.WebApplication.Models.Requests.Answers;

public sealed class RadioAnswerRequestModel : BaseAnswerRequestModel
{
    public Guid OptionId { get; set; }
}