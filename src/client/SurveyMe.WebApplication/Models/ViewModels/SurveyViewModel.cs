namespace SurveyMe.WebApplication.Models.ViewModels
{
    public sealed class SurveyViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public ICollection<QuestionViewModel> Questions { get; set; }

    }
}