using SurveyMe.WebApplication.Models.Requests.Questions;

namespace SurveyMe.WebApplication.Models.Requests.Surveys;

public sealed class SurveyRequestModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }
        
    public ICollection<QuestionRequestModel> Questions { get; set; }
}