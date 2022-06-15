using SurveyMe.WebApplication.Models.Responses.Questions;

namespace SurveyMe.WebApplication.Models.Responses.Surveys;

public sealed class SurveyResponseModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }
        
    public ICollection<QuestionResponseModel> Questions { get; set; }
}