using SurveyMe.Common.Pagination;
using SurveyMe.Data.Models;
using SurveyMe.DomainModels;
using SurveyMe.Repositories;

namespace SurveyMe.Data.Repositories.Abstracts
{
    public interface ISurveyRepository : IRepository<Survey>
    {
        Task<PagedResult<Survey>> GetSurveysAsync(int pageSize, int currentPage,
            string searchRequest, SortOrder sortOrder);

        Task<Survey> GetByIdAsync(Guid id);

        Task<SurveyAnswersStatistic> GetSurveyStatisticById(Guid id);
    }
}