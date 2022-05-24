using System.ComponentModel.DataAnnotations;
using SurveyMe.WebApplication.Models.ViewModels.Questions;

namespace SurveyMe.WebApplication.Models.ViewModels.Surveys
{
    public sealed class SurveyAddOrEditViewModel
    {
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public ICollection<QuestionAddOrEditViewModel> Questions { get; set; }
    }
}