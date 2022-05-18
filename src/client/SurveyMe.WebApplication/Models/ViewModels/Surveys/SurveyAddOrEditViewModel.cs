using SurveyMe.WebApplication.Models.ViewModels.Questions;

namespace SurveyMe.WebApplication.Models.ViewModels.Surveys
{
    public sealed class SurveyAddOrEditViewModel
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public ICollection<QuestionAddOrEditViewModel> Questions { get; set; }
    }
}