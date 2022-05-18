using SurveyMe.Data.Contracts;
using SurveyMe.DomainModels.Roles;

namespace SurveyMe.Data.Repositories.Abstracts;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role> GetByIdAsync(Guid id);
    Task<Role> GetByNameAsync(string name);
}