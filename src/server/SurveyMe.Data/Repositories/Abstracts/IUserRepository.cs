using SurveyMe.Common.Pagination;
using SurveyMe.Data.Contracts;
using SurveyMe.Data.Models;
using SurveyMe.DomainModels;

namespace SurveyMe.Data.Repositories.Abstracts;

public interface IUserRepository : IRepository<User>
{
    Task<PagedResult<UserWithSurveysCount>> GetUsersAsync(int pageSize, int currentPage,
        string searchRequest, SortOrder sortOrder);

    Task<User> GetByNameAsync(string name);

    Task<User> GetByIdAsync(Guid id);
}