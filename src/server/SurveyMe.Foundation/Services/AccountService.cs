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
    private readonly UserManager<User> _userManager;
    private readonly ISystemClock _systemClock;
    private readonly ISurveyMeUnitOfWork _unitOfWork;
    private readonly ITokenGenerator _tokenGenerator;


    public AccountService(UserManager<User> userManager, ISystemClock systemClock, 
        ISurveyMeUnitOfWork unitOfWork, ITokenGenerator tokenGenerator)
    {
        _userManager = userManager;
        _systemClock = systemClock;
        _unitOfWork = unitOfWork;
        _tokenGenerator = tokenGenerator;
    }


    public async Task<string> SignInAsync(string username, string password)
    {
        var user = await _unitOfWork.Users.GetByNameAsync(username); 
        
        var isCorrectPassword = await _userManager.CheckPasswordAsync(user, password);

        if (!isCorrectPassword)
        {
            throw new BadRequestException("Wrong username or password");
        }

        var token = _tokenGenerator.GenerateToken(user);

        return token;
    }

    public async Task<ServiceResult> RegisterAsync(User user, string password)
    {
        user.CreationTime = _systemClock.UtcNow;
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