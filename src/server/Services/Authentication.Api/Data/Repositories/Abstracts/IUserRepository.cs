using Authentication.Api.Data.Abstracts;
using Authentication.Api.Models.Users;

namespace Authentication.Api.Data.Repositories.Abstracts;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByNameAsync(string name);

    Task<User> GetByIdAsync(Guid id);
}