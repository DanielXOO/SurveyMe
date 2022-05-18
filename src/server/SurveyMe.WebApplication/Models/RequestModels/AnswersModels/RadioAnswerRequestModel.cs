namespace SurveyMe.WebApplication.Models.RequestModels.AnswersModels;

public sealed class RadioAnswerRequestModel : BaseAnswerRequestModel
{
    public Guid OptionId { get; set; }
}