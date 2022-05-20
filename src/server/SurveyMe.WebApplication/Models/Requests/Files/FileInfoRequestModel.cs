namespace SurveyMe.WebApplication.Models.Requests.Files;

public class FileInfoResponseModel
{
    public Guid Id { get; set; }

    public string ContentType { get; set; }

    public string Name { get; set; }
}