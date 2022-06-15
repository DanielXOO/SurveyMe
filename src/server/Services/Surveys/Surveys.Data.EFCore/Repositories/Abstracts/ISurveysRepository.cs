using SurveyMe.Common.Pagination;
using Surveys.Data.Core.Abstracts;
using Surveys.Models.Surveys;

namespace Surveys.Data.Repositories.Abstracts;

public interface ISurveysRepository : IRepository<Survey>
{
    Task<PagedResult<Survey>> GetSurveysAsync(int pageSize, int currentPage,
        string searchRequest, SortOrder sortOrder);

    Task<Survey> GetByIdAsync(Guid id);
}