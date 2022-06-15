using Microsoft.EntityFrameworkCore;
using SurveyMe.Data.Core;
using SurveyMe.Data.Repositories.Abstracts;
using SurveyMe.DomainModels.Answers;

namespace SurveyMe.Data.Repositories;

public sealed class AnswerRepository : Repository<SurveyAnswer>, IAnswerRepository
{
    public AnswerRepository(DbContext context) : base(context)
    {
    }


    public async Task<SurveyAnswer> GetByIdAsync(Guid id)
    {
        var answers = await GetAnswersQuery()
            .FirstOrDefaultAsync(answer => answer.Id == id);

        return answers;
    }

    public IEnumerable<SurveyAnswer> GetBySurveyId(Guid surveyId)
    {
        var answers = GetAnswersQuery().Where(answer => answer.SurveyId == surveyId);

        return answers;
    }

    
    private IQueryable<SurveyAnswer> GetAnswersQuery()
    {
        return Data
            .Include(answer => answer.QuestionsAnswers);
    }
}