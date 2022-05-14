using SurveyMe.DomainModels.Common;

namespace SurveyMe.WebApplication.Models.ViewModels
{
    public class QuestionViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        
        public QuestionType Type { get; set; }

        public ICollection<QuestionOptionViewModel> Options { get; set; }
    }
}