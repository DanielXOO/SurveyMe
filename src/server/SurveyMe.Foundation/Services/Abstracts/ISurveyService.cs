using System;
using System.Threading.Tasks;
using SurveyMe.Common.Pagination;
using SurveyMe.DomainModels;

namespace SurveyMe.Foundation.Services.Abstracts;

public interface ISurveyService
{
    Task<PagedResult<Survey>> GetSurveysAsync(int currentPage, int pageSize,
        SortOrder order, string searchRequest);

    Task DeleteSurveyAsync(Survey survey);

    Task<Survey> GetSurveyByIdAsync(Guid id);

    Task AddSurveyAsync(Survey survey, User author);

    Task UpdateSurveyAsync(Survey survey);
}