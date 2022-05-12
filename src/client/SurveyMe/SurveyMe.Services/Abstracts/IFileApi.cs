using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;
using FileInfo = SurveyMe.DomainModels.Common.FileInfo;

namespace SurveyMe.Services.Abstracts;

public interface IFileApi
{
    [Multipart]
    [Post("/files")]
    Task<FileInfo> UploadAsync(IFormFile fileModel);

    [Get("/files/{id}")]
    Task<FileContentResult> LoadAsync(Guid id);
}