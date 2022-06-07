using Authentication.Api.Data.Core.Abstracts;
using Authentication.Api.Data.Repositories.Abstracts;

namespace Authentication.Api.Data.Abstracts;

public interface IAuthenticationUnitOfWork : IUnitOfWork
{
    IUserRepository Users { get; }
    
    IRoleRepository Roles { get; }
}