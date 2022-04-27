using SurveyMe.DomainModels;
using SurveyMe.Repositories;

namespace SurveyMe.Data.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role> GetByIdAsync(Guid id);
        Task<Role> GetByNameAsync(string name);
    }
}