namespace SurveyMe.WebApplication.ViewModels
{
    public sealed class QuestionAnswerView
    {
        public Guid QuestionId { get; set; }

        public string TextAnswer { get; set; }

        public double RateAnswer { get; set; }

        public double ScaleAnswer { get; set; }
        
        public FileAnswerViewModel FileAnswer { get; set; }
        
        public ICollection<Guid> OptionIds { get; set; }
    }
}