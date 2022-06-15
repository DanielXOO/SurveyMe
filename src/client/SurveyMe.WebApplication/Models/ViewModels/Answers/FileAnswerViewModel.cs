using SurveyMe.WebApplication.Models.ViewModels.Files;

namespace SurveyMe.WebApplication.Models.ViewModels.Answers
{
    public class FileAnswerViewModel : BaseAnswerViewModel
    {
        public FileInfoViewModel File { get; set; }

        public Guid FileInfoId { get; set; }
    }
}