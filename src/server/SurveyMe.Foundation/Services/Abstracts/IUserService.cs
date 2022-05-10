using System;
using System.Threading.Tasks;
using SurveyMe.Common.Pagination;
using SurveyMe.DomainModels;
using SurveyMe.Foundation.Models;

namespace SurveyMe.Foundation.Services.Abstracts;

public interface IUserService
{
    Task<PagedResult<UserWithSurveysCount>> GetUsersAsync(int currentPage, int pageSize,
        SortOrder order, string searchRequest);

    Task<ServiceResult> DeleteUsersAsync(User user);

    Task<User> GetUserByIdAsync(Guid id);

    Task<ServiceResult> UpdateAsync(User user);
}