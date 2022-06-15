namespace Answers.Api.Models.Request.Answers;

public sealed class CheckboxAnswerRequestModel : BaseAnswerRequestModel
{
    public IEnumerable<Guid>? OptionIds { get; set; }
}