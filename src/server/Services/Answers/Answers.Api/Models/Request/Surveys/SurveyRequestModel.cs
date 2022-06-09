using Answers.Api.Models.Request.Questions;

namespace Answers.Api.Models.Request.Surveys;

public sealed class SurveyRequestModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }
        
    public ICollection<QuestionRequestModel> Questions { get; set; }
}