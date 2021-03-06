using Refit;
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


    public async Task<FileInfo> UploadAsync(StreamPart stream)
    {
        var fileInfo = await _fileApi.UploadAsync(stream);

        return fileInfo;
    }
}