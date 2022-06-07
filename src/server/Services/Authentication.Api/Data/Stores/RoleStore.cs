using Authentication.Api.Data.Abstracts;
using Authentication.Api.Models.Roles;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Api.Data.Stores;

public sealed class RoleStore : IRoleStore<Role>
{
    private readonly IAuthenticationUnitOfWork _unitOfWork;


    public RoleStore(IAuthenticationUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public void Dispose()
    {
    }

    public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _unitOfWork.Roles.CreateAsync(role);

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (role == null)
        {
            throw new ArgumentNullException(nameof(role));
        }

        await _unitOfWork.Roles.UpdateAsync(role);

        return IdentityResult.Success;
    }

    public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (role == null)
        {
            throw new ArgumentNullException(nameof(role));
        }

        return Task.FromResult(role.Id.ToString());
    }

    public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (role == null)
        {
            throw new ArgumentNullException(nameof(role));
        }

        return Task.FromResult(role.Name);
    }

    public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (role == null)
        {
            throw new ArgumentNullException(nameof(role));
        }

        role.Name = roleName;

        return Task.CompletedTask;
    }

    public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (role == null)
        {
            throw new ArgumentNullException(nameof(role));
        }

        return Task.FromResult(role.Name.ToUpper());
    }

    public Task SetNormalizedRoleNameAsync(Role role, string normalizedName,
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var role = await _unitOfWork.Roles.GetByIdAsync(Guid.Parse(roleId));

        return role;
    }

    public async Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var role = await _unitOfWork.Roles.GetByNameAsync(normalizedRoleName);

        return role;
    }
}