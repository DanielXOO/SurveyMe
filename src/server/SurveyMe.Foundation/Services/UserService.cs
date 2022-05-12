using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SurveyMe.Common.Exceptions;
using SurveyMe.Common.Extensions;
using SurveyMe.Common.Pagination;
using SurveyMe.Data;
using SurveyMe.DomainModels;
using SurveyMe.Foundation.Models;
using SurveyMe.Foundation.Services.Abstracts;

namespace SurveyMe.Foundation.Services;

public sealed class UserService : IUserService
{
    private readonly ISurveyMeUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;


    public UserService(ISurveyMeUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }


    public async Task<User> GetUserByIdAsync(Guid id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);

        if (user == null)
        {
            throw new NotFoundException("User do not exist");
        }
            
        return user;
    }

    public async Task<PagedResult<UserWithSurveysCount>> GetUsersAsync(int currentPage, int pageSize,
        SortOrder order, string searchRequest)
    {
        var usersPaged = await _unitOfWork.Users
            .GetUsersAsync(pageSize, currentPage, searchRequest, order);

        var usersPagedWithSurveysCount = usersPaged.MapPagedResult(user => new UserWithSurveysCount()
        {
            User = user.User,
            SurveysCount = user.SurveysCount
        });

        return usersPagedWithSurveysCount;
    }

    public async Task<ServiceResult> DeleteUsersAsync(User user)
    {
        var result = await _userManager.DeleteAsync(user);

        return ConvertToServiceResult(result);
    }

    public async Task<ServiceResult> UpdateAsync(User user)
    {
        var result = await _userManager.UpdateAsync(user);

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