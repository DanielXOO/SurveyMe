using Refit;
using FileInfo = SurveyMe.DomainModels.Common.FileInfo;
using File = SurveyMe.DomainModels.Common.File;

namespace SurveyMe.Data.Abstracts;

[Headers("Authorization: Bearer")]
public interface IFileApi
{
    [Multipart]
    [Post("/files")]
    Task<FileInfo> UploadAsync([AliasAs("file")]StreamPart stream);
    
    [Get("/files/{id}")]
    Task<File> LoadAsync(Guid id);
}