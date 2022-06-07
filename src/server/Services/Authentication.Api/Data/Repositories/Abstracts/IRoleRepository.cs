using Authentication.Api.Data.Core.Abstracts;
using Authentication.Api.Models.Roles;

namespace Authentication.Api.Data.Repositories.Abstracts;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role> GetByIdAsync(Guid id);
    
    Task<Role> GetByNameAsync(string name);
}