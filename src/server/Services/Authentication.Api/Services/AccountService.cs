using Authentication.Api.Common;
using Authentication.Api.Data;
using Authentication.Api.Models.Roles;
using Authentication.Api.Models.Users;
using Authentication.Api.Services.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Api.Services;

public sealed class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly ISystemClock _systemClock;
    private readonly IAuthenticationUnitOfWork _unitOfWork;


    public AccountService(UserManager<User> userManager, ISystemClock systemClock, 
        IAuthenticationUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _systemClock = systemClock;
        _unitOfWork = unitOfWork;
    }


    public async Task<ServiceResult> RegisterAsync(User user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
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