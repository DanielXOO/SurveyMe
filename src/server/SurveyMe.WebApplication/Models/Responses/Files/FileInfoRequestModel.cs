namespace SurveyMe.WebApplication.Models.Responses.Files;

public class FileInfoResponseModel
{
    public Guid FileId { get; set; }

    public string ContentType { get; set; }

    public string Name { get; set; }
}