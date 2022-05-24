using SurveyMe.Data.Contracts;
using FileInfo = SurveyMe.DomainModels.Files.FileInfo;

namespace SurveyMe.Data.Repositories.Abstracts;

public interface IFileRepository : IRepository<FileInfo>
{
    Task<FileInfo> GetByIdAsync(Guid id);
}