using Authentication.Data.Core.Abstracts;
using Authentication.Users;

namespace Authentication.Data.Repositories.Abstracts;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByNameAsync(string name);

    Task<User> GetByIdAsync(Guid id);
}