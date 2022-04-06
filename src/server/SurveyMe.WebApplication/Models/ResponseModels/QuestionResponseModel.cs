using SurveyMe.DomainModels;

namespace SurveyMe.WebApplication.Models.ResponseModels;

public sealed class QuestionResponseModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }
        
    public QuestionType Type { get; set; }

    public ICollection<QuestionOptionResponseModel> Options { get; set; }
}