using Surveys.Api.Common.Pagination;
using Surveys.Api.Data.Core.Abstracts;
using Surveys.Api.Models.Surveys;

namespace Surveys.Api.Data.Repositories.Abstracts;

public interface ISurveysRepository : IRepository<Survey>
{
    Task<PagedResult<Survey>> GetSurveysAsync(int pageSize, int currentPage,
        string searchRequest, SortOrder sortOrder);

    Task<Survey> GetByIdAsync(Guid id);
}