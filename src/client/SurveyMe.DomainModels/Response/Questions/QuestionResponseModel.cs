using SurveyMe.DomainModels.Common;

namespace SurveyMe.DomainModels.Response.Questions;

public sealed class QuestionResponseModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }
        
    public QuestionType Type { get; set; }

    public ICollection<QuestionOptionResponseModel> Options { get; set; }
}