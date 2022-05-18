namespace SurveyMe.DomainModels.Common;

public sealed class FileInfo
{
    public Guid Id { get; set; }

    public string ContentType { get; set; }

    public string Name { get; set; }
}