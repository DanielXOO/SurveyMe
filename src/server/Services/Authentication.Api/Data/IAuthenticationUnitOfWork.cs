using Authentication.Api.Data.Abstracts;
using Authentication.Api.Data.Repositories.Abstracts;

namespace Authentication.Api.Data;

public interface IAuthenticationUnitOfWork : IUnitOfWork
{
    IUserRepository Users { get; }
    
    IRoleRepository Roles { get; }
}