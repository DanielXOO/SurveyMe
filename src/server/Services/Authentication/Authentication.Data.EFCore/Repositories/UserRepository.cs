using Authentication.Data.Core;
using Authentication.Data.Repositories.Abstracts;
using Authentication.Users;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Data.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    
    public UserRepository(DbContext dbContext) : base(dbContext)
    {
    }
    
    
    public async Task<User> GetByNameAsync(string name)
    {
        var user = await GetQuery().FirstOrDefaultAsync(user => user.UserName == name);

        return user;
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        var user = await GetQuery().FirstOrDefaultAsync(user => user.Id == id);

        return user;
    }


    private IQueryable<User> GetQuery()
    {
        return Data.Include(p => p.Roles);
    }
}