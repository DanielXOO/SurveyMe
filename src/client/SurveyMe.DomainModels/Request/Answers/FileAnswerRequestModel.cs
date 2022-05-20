using SurveyMe.DomainModels.Request.Files;

namespace SurveyMe.DomainModels.Request.Answers;

public class FileAnswerRequestModel : BaseAnswerRequestModel
{
    public FileInfoRequestModel? File { get; set; }
    
    public Guid FileInfoId { get; set; }
}