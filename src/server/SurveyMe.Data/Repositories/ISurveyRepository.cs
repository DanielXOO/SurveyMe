using SurveyMe.Common.Pagination;
using SurveyMe.DomainModels;
using SurveyMe.Repositories;

namespace SurveyMe.Data.Repositories
{
    public interface ISurveyRepository : IRepository<Survey>
    {
        Task<PagedResult<Survey>> GetSurveysAsync(int pageSize, int currentPage,
            string searchRequest, SortOrder sortOrder);
        
        Task<Survey> GetByIdAsync(Guid id);
    }
}