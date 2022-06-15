namespace SurveyMe.WebApplication.Models.ViewModels.Surveys
{
    public sealed class SurveyWithLinksViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime LastChangeDate { get; set; }

        public string? SurveyLink { get; set; }

        public string? ResultLink { get; set; }
    }
}