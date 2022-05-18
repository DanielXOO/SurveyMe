namespace SurveyMe.DomainModels.Common;

public sealed class File
{
    public Stream Data { get; set; }

    public FileInfo Info { get; set; }
}