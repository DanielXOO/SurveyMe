namespace SurveyMe.WebApplication.Models.ResponseModels;

public sealed class SurveyWithLinksResponseModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime LastChangeDate { get; set; }

    public string? SurveyLink { get; set; }

    public string ResultLink { get; set; }
}