using FileInfo = SurveyMe.DomainModels.Files.FileInfo;

namespace SurveyMe.DomainModels.Answers;

public sealed class FileAnswer
{
    public Guid Id { get; set; }

    public FileInfo FileInfo { get; set; }

    public Guid QuestionAnswerId { get; set; }

    public QuestionAnswer QuestionAnswer { get; set; }
}