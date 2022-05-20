using Refit;
using SurveyMe.Data.Abstracts;
using SurveyMe.Services.Abstracts;
using FileInfo = SurveyMe.DomainModels.Common.FileInfo;
using File = SurveyMe.DomainModels.Common.File;

namespace SurveyMe.Services;

public sealed class FileService : IFileService
{
    private readonly IFileApi _fileApi;

    public FileService(IFileApi fileApi)
    {
        _fileApi = fileApi;
    }


    public async Task<FileInfo> UploadAsync(StreamPart file)
    {
        var fileInfo = await _fileApi.UploadAsync(file);

        return fileInfo;
    }

    public async Task<File> LoadAsync(Guid id)
    {
        var file = await _fileApi.LoadAsync(id);

        return file;
    }
}