using Authentication.Api.Data.Core;
using Authentication.Api.Data.Repositories;
using Authentication.Api.Data.Repositories.Abstracts;
using Authentication.Api.Models.Roles;
using Authentication.Api.Models.Users;

namespace Authentication.Api.Data;

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