using Refit;
using FileInfo = SurveyMe.DomainModels.Common.FileInfo;
using File = SurveyMe.DomainModels.Common.File;

namespace SurveyMe.Services.Abstracts;

public interface IFileService
{
    Task<FileInfo> UploadAsync(StreamPart file);
}