using SurveyMe.Common.Pagination;
using SurveyMe.Data.Core;
using Microsoft.EntityFrameworkCore;
using SurveyMe.Data.Repositories.Abstracts;
using SurveyMe.DomainModels.Surveys;

namespace SurveyMe.Data.Repositories;

public sealed class SurveyRepository : Repository<Survey>, ISurveyRepository
{
    public SurveyRepository(DbContext context) : base(context)
    {
    }


    public async Task<PagedResult<Survey>> GetSurveysAsync(int pageSize, int currentPage, string searchRequest,
        SortOrder sortOrder)
    {
        var surveys = GetSurveysQuery();

        if (!string.IsNullOrEmpty(searchRequest))
        {
            surveys = surveys.Where(survey => survey.Name.Contains(searchRequest));
        }

        switch (sortOrder)
        {
            case SortOrder.Descending:
                surveys = surveys.OrderByDescending(survey => survey.Name);
                break;
            case SortOrder.Ascending:
                surveys = surveys.OrderBy(survey => survey.Name);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(sortOrder), sortOrder, "Unknown sort order value");
        }

        var result = await surveys.ToPagedResultAsync(pageSize, currentPage);

        return result;
    }

    public async Task<Survey> GetByIdAsync(Guid id)
    {
        var survey = await GetSurveysQuery()
            .FirstOrDefaultAsync(survey => survey.Id == id);

        return survey;
    }

        

    private IQueryable<Survey> GetSurveysQuery()
    {
        return Data.Include(survey => survey.Questions)
            .ThenInclude(question => question.Options);
    }
}