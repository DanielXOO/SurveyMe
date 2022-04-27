using SurveyMe.Repositories;

using FileInfo = SurveyMe.DomainModels.FileInfo;

namespace SurveyMe.Data.Repositories
{
    public interface IFileRepository : IRepository<FileInfo>
    {
        Task<FileInfo> GetByIdAsync(Guid id);
    }
}