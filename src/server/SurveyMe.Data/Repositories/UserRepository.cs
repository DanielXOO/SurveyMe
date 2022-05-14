using SurveyMe.Common.Pagination;
using SurveyMe.Data.Core;
using Microsoft.EntityFrameworkCore;
using SurveyMe.Data.Models;
using SurveyMe.Data.Repositories.Abstracts;
using SurveyMe.DomainModels;

namespace SurveyMe.Data.Repositories;

public sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(DbContext context) : base(context)
    {
    }


    public async Task<PagedResult<UserWithSurveysCount>> GetUsersAsync(int pageSize, int currentPage,
        string searchRequest, SortOrder sortOrder)
    {
        var users = GetUsersQuery();

        if (!string.IsNullOrEmpty(searchRequest))
        {
            users = users.Where(user => user.DisplayName.Contains(searchRequest));
        }

        users = sortOrder switch
        {
            SortOrder.Descending => users.OrderByDescending(user => user.DisplayName),
            SortOrder.Ascending => users.OrderBy(user => user.DisplayName),
            _ => throw new ArgumentOutOfRangeException(nameof(sortOrder), 
                sortOrder, "Unknown sort order value")
        };

        var userWithSurveysCount = users.Select(user =>
            new UserWithSurveysCount
            {
                User = user,
                SurveysCount = user.Surveys.Count
            });

        var result = await userWithSurveysCount.ToPagedResultAsync(pageSize, currentPage);

        return result;
    }

    public async Task<User> GetByNameAsync(string name)
    {
        var user = await GetUsersQuery()
            .FirstOrDefaultAsync(user => user.UserName == name);

        return user;
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        var user = await GetUsersQuery()
            .FirstOrDefaultAsync(user => user.Id == id);

        return user;
    }


    private IQueryable<User> GetUsersQuery()
    {
        return Data
            .Include(user => user.Roles)
            .Include(user => user.Surveys);
    }
}