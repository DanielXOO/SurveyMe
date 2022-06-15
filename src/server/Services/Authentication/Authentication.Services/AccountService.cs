using Authentication.Roles;
using Authentication.Services.Abstracts;
using Authentication.Users;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Services;

public sealed class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;


    public AccountService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }


    public async Task<ServiceResult> RegisterAsync(User user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return ConvertToServiceResult(result);
        }
        
        await _userManager.AddToRoleAsync(user, RoleNames.User);
        
        return ConvertToServiceResult(result);
    }
    
    
    private static ServiceResult ConvertToServiceResult(IdentityResult result)
    {
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(error => error.Description).ToArray();

            return ServiceResult.CreateFailed(errors);
        }

        return ServiceResult.CreateSuccessful();
    }
}