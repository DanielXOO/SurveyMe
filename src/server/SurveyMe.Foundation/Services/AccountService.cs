using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SurveyMe.Common.Exceptions;
using SurveyMe.Common.Time;
using SurveyMe.Data;
using SurveyMe.DomainModels;
using SurveyMe.Foundation.Services.Abstracts;

namespace SurveyMe.Foundation.Services;

public sealed class AccountService : IAccountService
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly ISystemClock _systemClock;
    private readonly ISurveyMeUnitOfWork _unitOfWork;
    private readonly ITokenGenerator _tokenGenerator;


    public AccountService(SignInManager<User> signInManager,
        UserManager<User> userManager, ISystemClock systemClock, 
        ISurveyMeUnitOfWork unitOfWork, ITokenGenerator tokenGenerator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _systemClock = systemClock;
        _unitOfWork = unitOfWork;
        _tokenGenerator = tokenGenerator;
    }


    public async Task<ServiceResult> SignInAsync(string username, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(username, password, true, false);

        return ConvertToServiceResult(result);
    }

    public async Task<ServiceResult> RegisterAsync(User user, string password)
    {
        user.CreationTime = _systemClock.UtcNow;
        var result = await _userManager.CreateAsync(user, password);
        await _userManager.AddToRoleAsync(user, RoleNames.User);

        return ConvertToServiceResult(result);
    }

    public async Task<string> GenerateTokenAsync(string userName)
    {
        var user = await _unitOfWork.Users.GetByNameAsync(userName);

        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        
        var token = _tokenGenerator.GenerateToken(user);

        return token;
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
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

    private static ServiceResult ConvertToServiceResult(SignInResult result)
    {
        if (!result.Succeeded)
        {
            return ServiceResult.CreateFailed(new[]
            {
                "Login or Password is not correct"
            });
        }

        return ServiceResult.CreateSuccessful();
    }
}