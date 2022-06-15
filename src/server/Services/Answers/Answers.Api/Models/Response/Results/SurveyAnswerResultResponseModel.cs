namespace Answers.Api.Models.Response.Results;

public sealed class SurveyAnswerResultResponseModel
{
    public string Name { get; set; }

    public Guid UserId { get; set; }
    
    public ICollection<BaseAnswerResultResponseModel> QuestionAnswers { get; set; }
}