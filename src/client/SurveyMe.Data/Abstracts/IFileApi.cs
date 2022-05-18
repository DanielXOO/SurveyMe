using Refit;
using FileInfo = SurveyMe.DomainModels.Common.FileInfo;
using File = SurveyMe.DomainModels.Common.File;

namespace SurveyMe.Data.Abstracts;

[Headers("Authorization: Bearer")]
public interface IFileApi
{
    [Post("/files")]
    Task<FileInfo> UploadAsync([Body]File fileModel);

    [Get("/files/{id}")]
    Task<File> LoadAsync(Guid id);
}