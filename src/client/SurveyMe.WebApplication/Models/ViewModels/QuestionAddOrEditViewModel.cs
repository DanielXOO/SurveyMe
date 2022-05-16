namespace SurveyMe.WebApplication.Models.ViewModels
{
    public sealed class QuestionAddOrEditViewModel
    {
        public string Title { get; set; }

        public int Type { get; set; }
        
        public ICollection<string> Options { get; set; }
    }
}