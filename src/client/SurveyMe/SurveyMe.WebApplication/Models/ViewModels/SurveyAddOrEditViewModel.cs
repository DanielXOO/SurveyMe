using System.ComponentModel.DataAnnotations;

namespace SurveyMe.WebApplication.Models.ViewModels
{
    public sealed class SurveyAddOrEditViewModel
    {
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public ICollection<QuestionCreateOrEditViewModel> Questions { get; set; }
    }
}