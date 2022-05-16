namespace SurveyMe.DomainModels.Request;

public sealed class QuestionRequestModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }
    
    public int Type { get; set; }

    public ICollection<QuestionOptionRequestModel> Options { get; set; }
}