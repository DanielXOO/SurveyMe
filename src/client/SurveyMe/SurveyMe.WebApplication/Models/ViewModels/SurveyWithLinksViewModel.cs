namespace SurveyMe.WebApplication.Models.ViewModels
{
    public sealed class SurveyWithLinksViewModel
    {
        public Guid SurveyId { get; set; }

        public string Name { get; set; }

        public DateTime UpdateDate { get; set; }

        public string SurveyLink { get; set; }

        public string ResultsLink { get; set; }
    }
}