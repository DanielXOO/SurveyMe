using Answers.Api.Data.Core;
using Answers.Api.Data.Repositories.Abstracts;
using Answers.Api.Models.Answers;
using Microsoft.EntityFrameworkCore;

namespace Answers.Api.Data.Repositories;

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

    private IQueryable<SurveyAnswer> GetAnswersQuery()
    {
        return Data
            .Include(answer => answer.QuestionsAnswers);
    }
}