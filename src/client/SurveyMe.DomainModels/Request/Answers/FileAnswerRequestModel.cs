using SurveyMe.DomainModels.Request.Files;

namespace SurveyMe.DomainModels.Request.Answers;

public class FileAnswerRequestModel : BaseAnswerRequestModel
{    
    public Guid FileInfoId { get; set; }
}