using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FileInfo = SurveyMe.DomainModels.Common.FileInfo;

namespace SurveyMe.Services.Abstracts;

public interface IFileService
{
    Task<FileInfo> UploadAsync(IFormFile fileModel);

    Task<FileContentResult> LoadAsync(Guid id);
}