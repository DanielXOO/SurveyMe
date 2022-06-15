using SurveyMe.DomainModels.Answers;

namespace SurveyMe.DomainModels.Files;

public sealed class FileInfo
{
    public Guid FileId { get; set; }
    
    public string ContentType { get; set; }

    public string Name { get; set; }

    public FileAnswer FileAnswer { get; set; }
}