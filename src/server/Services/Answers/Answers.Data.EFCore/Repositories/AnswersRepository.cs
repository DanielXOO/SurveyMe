using Answers.Data.Core;
using Answers.Data.Repositories.Abstracts;
using Answers.Models.Answers;
using Microsoft.EntityFrameworkCore;
using SurveyMe.Common.Pagination;

namespace Answers.Data.Repositories;

public sealed class AnswersRepository : Repository<SurveyAnswer>, IAnswersRepository
{
    public AnswersRepository(DbContext context) : base(context)
    {
    }


    public async Task<SurveyAnswer> GetByIdAsync(Guid id)
    {
        var answers = await GetAnswersQuery()
            .FirstOrDefaultAsync(answer => answer.Id == id);

        return answers;
    }

    public async Task<PagedResult<SurveyAnswer>> GetSurveyAnswersAsync(int currentPage, int pageSize, Guid surveyId)
    {
        var answers = GetAnswersQuery();

        var result = await answers.Where(answer => answer.SurveyId == surveyId)
            .ToPagedResultAsync(pageSize, currentPage);

        return result;
    }

    private IQueryable<SurveyAnswer> GetAnswersQuery()
    {
        return Data
            .Include(answer => answer.QuestionsAnswers);
    }
}