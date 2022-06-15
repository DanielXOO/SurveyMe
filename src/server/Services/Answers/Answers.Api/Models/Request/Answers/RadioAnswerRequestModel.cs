namespace Answers.Api.Models.Request.Answers;

public sealed class RadioAnswerRequestModel : BaseAnswerRequestModel
{
    public Guid OptionId { get; set; }
}