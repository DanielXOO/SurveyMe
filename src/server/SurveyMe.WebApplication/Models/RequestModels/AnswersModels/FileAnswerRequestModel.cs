using SurveyMe.WebApplication.Models.RequestModels.FileModels;

namespace SurveyMe.WebApplication.Models.RequestModels.AnswersModels;

public class FileAnswerRequestModel : BaseAnswerRequestModel
{
    public FileInfoRequestModel? File { get; set; }

}