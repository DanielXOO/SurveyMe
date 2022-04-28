using SurveyMe.Repositories;
using FileInfo = SurveyMe.DomainModels.FileInfo;

namespace SurveyMe.Data.Repositories.Abstracts
{
    public interface IFileRepository : IRepository<FileInfo>
    {
        Task<FileInfo> GetByIdAsync(Guid id);
    }
}