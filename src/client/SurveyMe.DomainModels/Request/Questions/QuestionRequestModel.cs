using SurveyMe.DomainModels.Common;

namespace SurveyMe.DomainModels.Request.Questions;

public sealed class QuestionRequestModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }
    
    public QuestionType Type { get; set; }

    public ICollection<QuestionOptionRequestModel> Options { get; set; }
}