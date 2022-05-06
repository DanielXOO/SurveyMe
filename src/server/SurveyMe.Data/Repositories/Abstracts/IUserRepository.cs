using SurveyMe.Common.Pagination;
using SurveyMe.Data.Models;
using SurveyMe.DomainModels;
using SurveyMe.Repositories;

namespace SurveyMe.Data.Repositories.Abstracts
{
    public interface IUserRepository : IRepository<User>
    {
        Task<PagedResult<UserWithSurveysCount>> GetUsersAsync(int pageSize, int currentPage,
            string searchRequest, SortOrder sortOrder);

        Task<User> GetByNameAsync(string name);

        Task<User> GetByIdAsync(Guid id);
    }
}