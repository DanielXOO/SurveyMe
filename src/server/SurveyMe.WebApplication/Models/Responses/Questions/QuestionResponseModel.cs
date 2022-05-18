using SurveyMe.DomainModels.Questions;

namespace SurveyMe.WebApplication.Models.Responses.Questions;

public sealed class QuestionResponseModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }
        
    public QuestionType Type { get; set; }

    public ICollection<QuestionOptionResponseModel> Options { get; set; }
}