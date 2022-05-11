using SurveyMe.DomainModels.Common;

namespace SurveyMe.WebApplication.ViewModels
{
    public class QuestionViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        
        public QuestionType Type { get; set; }

        public ICollection<QuestionOptionsViewModel> Options { get; set; }
    }
}