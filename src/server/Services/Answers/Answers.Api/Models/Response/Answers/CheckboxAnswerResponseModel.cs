namespace Answers.Api.Models.Response.Answers;

public sealed class CheckboxAnswerResponseModel : BaseAnswerResponseModel
{
    public IEnumerable<Guid>? OptionIds { get; set; }
}