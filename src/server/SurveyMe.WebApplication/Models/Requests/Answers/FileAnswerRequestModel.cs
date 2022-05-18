using SurveyMe.WebApplication.Models.Requests.Files;

namespace SurveyMe.WebApplication.Models.Requests.Answers;

public class FileAnswerRequestModel : BaseAnswerRequestModel
{
    public FileInfoRequestModel? File { get; set; }

}