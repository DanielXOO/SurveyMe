using FileInfo = SurveyMe.DomainModels.Common.FileInfo;
using File = SurveyMe.DomainModels.Common.File;

namespace SurveyMe.Services.Abstracts;

public interface IFileService
{
    Task<FileInfo> UploadAsync(File fileModel);

    Task<File> LoadAsync(Guid id);
}