using SurveyMe.Common.Pagination;
using SurveyMe.Data.Contracts;
using SurveyMe.DomainModels;

namespace SurveyMe.Data.Repositories.Abstracts;

public interface ISurveyRepository : IRepository<Survey>
{
    Task<PagedResult<Survey>> GetSurveysAsync(int pageSize, int currentPage,
        string searchRequest, SortOrder sortOrder);

    Task<Survey> GetByIdAsync(Guid id);
}