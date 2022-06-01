using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SurveyMe.Common.Extensions;
using SurveyMe.Common.Pagination;
using SurveyMe.Data;
using SurveyMe.DomainModels.Users;
using SurveyMe.Foundation.Exceptions;
using SurveyMe.Foundation.Models.Users;
using SurveyMe.Foundation.Services.Abstracts;

namespace SurveyMe.Foundation.Services;

public sealed class UserService : IUserService
{
    private readonly ISurveyMeUnitOfWork _unitOfWork;


    public UserService(ISurveyMeUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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

        var usersPagedWithSurveysCount = usersPaged.MapPagedResult(user => new UserWithSurveysCount
        {
            User = user.User,
            SurveysCount = user.SurveysCount
        });

        return usersPagedWithSurveysCount;
    }

    public async Task DeleteUsersAsync(User user)
    { 
        await _unitOfWork.Users.DeleteAsync(user);
    }

    public async Task UpdateAsync(User user)
    {
        await _unitOfWork.Users.UpdateAsync(user);
    }
}