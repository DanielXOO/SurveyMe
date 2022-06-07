using Microsoft.EntityFrameworkCore;
using Surveys.Api.Common.Pagination;

namespace Surveys.Api.Data.Core;

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