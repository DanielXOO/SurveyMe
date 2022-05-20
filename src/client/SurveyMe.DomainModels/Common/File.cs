using Refit;

namespace SurveyMe.DomainModels.Common;

public sealed class File
{
    public StreamPart Data { get; set; }

    public FileInfo Info { get; set; }
}