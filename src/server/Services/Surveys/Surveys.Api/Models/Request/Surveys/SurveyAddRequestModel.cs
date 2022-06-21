using Surveys.Api.Models.Request.Questions;

namespace Surveys.Api.Models.Request.Surveys;

public sealed class SurveyAddRequestModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }
        
    public ICollection<QuestionAddRequestModel> Questions { get; set; }
}