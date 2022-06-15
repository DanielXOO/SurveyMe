using Refit;
using FileInfo = SurveyMe.DomainModels.Common.FileInfo;

namespace SurveyMe.Data.Abstracts;

[Headers("Authorization: Bearer")]
public interface IFileApi
{
    [Multipart]
    [Post("/files")]
    Task<FileInfo> UploadAsync([AliasAs("file")]StreamPart stream);
}