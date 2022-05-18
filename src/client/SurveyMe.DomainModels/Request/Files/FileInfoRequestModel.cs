namespace SurveyMe.DomainModels.Request.Files;

public class FileInfoRequestModel
{
    public Guid Id { get; set; }

    public string ContentType { get; set; }

    public string Name { get; set; }
}