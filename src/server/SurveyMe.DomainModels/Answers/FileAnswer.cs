using FileInfo = SurveyMe.DomainModels.Files.FileInfo;

namespace SurveyMe.DomainModels.Answers;

public sealed class FileAnswer : BaseAnswer
{
    public FileInfo FileInfo { get; set; }
    
    public Guid FileInfoId { get; set; }
}