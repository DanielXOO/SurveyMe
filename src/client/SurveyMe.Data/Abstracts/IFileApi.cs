using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;
using FileInfo = SurveyMe.DomainModels.Common.FileInfo;

namespace SurveyMe.Data.Abstracts;

public interface IFileApi
{
    [Post("/files")]
    Task<FileInfo> UploadAsync([Body]IFormFile fileModel);

    [Get("/files/{id}")]
    Task<FileContentResult> LoadAsync(Guid id);
}