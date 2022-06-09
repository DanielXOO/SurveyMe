using Authentication.Data.Core;
using Authentication.Data.Repositories.Abstracts;
using Authentication.Roles;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Data.Repositories;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Role> GetByIdAsync(Guid id)
    {
        var role = await Data
            .FirstOrDefaultAsync(role => role.Id == id);

        return role;
    }

    public async Task<Role> GetByNameAsync(string name)
    {
        var role = await Data
            .FirstOrDefaultAsync(user => user.Name == name);

        return role;
    }
}