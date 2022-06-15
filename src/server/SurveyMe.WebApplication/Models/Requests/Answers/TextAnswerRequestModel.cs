namespace SurveyMe.WebApplication.Models.Requests.Answers;

public sealed class TextAnswerRequestModel : BaseAnswerRequestModel
{
    public string? TextAnswer { get; set; }
}