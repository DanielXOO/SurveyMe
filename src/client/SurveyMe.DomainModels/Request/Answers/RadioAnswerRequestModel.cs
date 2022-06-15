namespace SurveyMe.DomainModels.Request.Answers;

public sealed class RadioAnswerRequestModel : BaseAnswerRequestModel
{
    public Guid OptionId { get; set; }
}