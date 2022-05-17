using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.Data.Abstracts;
using SurveyMe.Services.Abstracts;
using FileInfo = SurveyMe.DomainModels.Common.FileInfo;

namespace SurveyMe.Services;

public sealed class FileService : IFileService
{
    private readonly IFileApi _fileApi;

    public FileService(IFileApi fileApi)
    {
        _fileApi = fileApi;
    }


    public async Task<FileInfo> UploadAsync(IFormFile fileModel)
    {
        var fileInfo = await _fileApi.UploadAsync(fileModel);

        return fileInfo;
    }

    public async Task<FileContentResult> LoadAsync(Guid id)
    {
        var file = await _fileApi.LoadAsync(id);

        return file;
    }
}