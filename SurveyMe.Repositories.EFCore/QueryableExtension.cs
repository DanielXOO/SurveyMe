using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SurveyMe.Common.Pagination;

namespace SurveyMe.Repositories.EFCore
{
    public static class QueryableExtension
    {
        public static async Task<PagedResult<TModel>> ToPagedResultAsync<TModel>(this IQueryable<TModel> data,
            int pageSize, int page)
        {
            var totalItems = await data.CountAsync();

            var items = await data
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<TModel>(items, pageSize, page, totalItems);
        }
    }
}