using System.ComponentModel.DataAnnotations;

namespace SurveyMe.WebApplication.Models.ViewModels
{
    public sealed class SurveyAddOrEditViewModel
    {
        [Required]
        public string Name { get; set; }

        public ICollection<QuestionAddOrEditViewModel> Questions { get; set; }
    }
}