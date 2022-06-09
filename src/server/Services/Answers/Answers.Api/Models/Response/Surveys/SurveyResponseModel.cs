using Answers.Api.Models.Response.Questions;

namespace Answers.Api.Models.Response.Surveys;

public sealed class SurveyResponseModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }
        
    public ICollection<QuestionResponseModel> Questions { get; set; }
}