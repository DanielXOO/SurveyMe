using Surveys.Api.Models.Request.Questions;

namespace Surveys.Api.Models.Request.Surveys;

public sealed class SurveyEditRequestModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }
        
    public ICollection<QuestionEditRequestModel> Questions { get; set; }
}