namespace SurveyMe.DomainModels.Answers;

public sealed class FileAnswer : BaseAnswer
{
    public string ContentType { get; set; }

    public string Name { get; set; }
}