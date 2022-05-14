namespace SurveyMe.DomainModels.Request;

public sealed class QuestionOptionRequestModel
{
    public Guid Id { get; set; }

    public string Text { get; set; }
}