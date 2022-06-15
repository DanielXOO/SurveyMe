using Authentication.Data.Abstracts;
using Authentication.Data.Core;
using Authentication.Data.Repositories;
using Authentication.Data.Repositories.Abstracts;
using Authentication.Roles;
using Authentication.Users;

namespace Authentication.Data;

public class AuthenticationUnitOfWork : UnitOfWork, IAuthenticationUnitOfWork
{
    public IUserRepository Users
        => (IUserRepository)GetRepository<User>();
    
    public IRoleRepository Roles
        => (IRoleRepository)GetRepository<Role>();

    
    public AuthenticationUnitOfWork(AuthenticationDbContext dbContext) : base(dbContext)
    {
        AddSpecificRepository<User, UserRepository>();
        AddSpecificRepository<Role, RoleRepository>();
    }
}