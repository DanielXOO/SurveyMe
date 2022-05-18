using SurveyMe.DomainModels;

namespace SurveyMe.WebApplication.Models.RequestModels.QuestionModels;

public sealed class QuestionRequestModel
{
    public string Title { get; set; }
        
    public QuestionType Type { get; set; }

    public ICollection<QuestionOptionRequestModel> Options { get; set; }
}