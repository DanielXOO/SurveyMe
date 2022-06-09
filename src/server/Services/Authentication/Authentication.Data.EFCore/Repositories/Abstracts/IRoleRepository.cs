using Authentication.Data.Core.Abstracts;
using Authentication.Roles;

namespace Authentication.Data.Repositories.Abstracts;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role> GetByIdAsync(Guid id);
    
    Task<Role> GetByNameAsync(string name);
}