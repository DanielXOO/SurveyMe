namespace SurveyMe.DomainModels.Request.Answers;

public sealed class TextAnswerRequestModel : BaseAnswerRequestModel
{
    public string? TextAnswer { get; set; }
}