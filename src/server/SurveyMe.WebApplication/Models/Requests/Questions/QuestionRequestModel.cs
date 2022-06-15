using SurveyMe.DomainModels.Questions;

namespace SurveyMe.WebApplication.Models.Requests.Questions;

public sealed class QuestionRequestModel
{
    public string Title { get; set; }
        
    public QuestionType Type { get; set; }

    public ICollection<QuestionOptionRequestModel> Options { get; set; }
}