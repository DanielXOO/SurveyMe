namespace SurveyMe.DomainModels.Request;

public sealed class FileAnswerRequestModel
{
    public Guid FileId { get; set; }

    public string? ContentType { get; set; }

    public string? Name { get; set; }
}