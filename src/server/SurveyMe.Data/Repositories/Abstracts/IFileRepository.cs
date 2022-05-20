using SurveyMe.Data.Contracts;
using SurveyMe.DomainModels.Answers;

namespace SurveyMe.Data.Repositories.Abstracts;

public interface IFileRepository : IRepository<FileAnswer>
{
    Task<FileAnswer> GetByIdAsync(Guid id);
}