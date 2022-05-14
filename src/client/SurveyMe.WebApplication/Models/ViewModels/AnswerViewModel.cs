namespace SurveyMe.WebApplication.Models.ViewModels
{
    public sealed class AnswerViewModel
    {
        public Guid SurveyId { get; set; }
        
        public ICollection<QuestionAnswerView> Questions { get; set; }
    }
}