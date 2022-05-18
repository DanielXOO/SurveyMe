namespace SurveyMe.WebApplication.Models.ViewModels.Surveys
{
    public class SurveyDeleteViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ReturnUrl { get; set; }
    }
}