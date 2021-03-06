namespace SurveyMe.WebApplication.Models.ViewModels.Answers
{
    public sealed class AnswerViewModel
    {
        public Guid SurveyId { get; set; }
        
        public ICollection<BaseAnswerViewModel> QuestionsAnswers { get; set; }
    }
}