using Refit;
using FileInfo = SurveyMe.DomainModels.Common.FileInfo;

namespace SurveyMe.Services.Abstracts;

public interface IFileService
{
    Task<FileInfo> UploadAsync(StreamPart file);
}