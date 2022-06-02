using SurveyMe.DomainModels.Response.Questions;

namespace SurveyMe.DomainModels.Response.Surveys;

public sealed class SurveyResponseModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }
        
    public ICollection<QuestionResponseModel> Questions { get; set; }
}