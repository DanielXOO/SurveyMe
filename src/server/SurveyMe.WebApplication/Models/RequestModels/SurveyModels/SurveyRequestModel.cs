using SurveyMe.WebApplication.Models.RequestModels.QuestionModels;

namespace SurveyMe.WebApplication.Models.RequestModels.SurveyModels;

public sealed class SurveyRequestModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }
        
    public ICollection<QuestionRequestModel> Questions { get; set; }
}