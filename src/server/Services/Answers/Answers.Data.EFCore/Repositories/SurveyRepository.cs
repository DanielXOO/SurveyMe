using Answers.Data.Core;
using Answers.Data.Repositories.Abstracts;
using Answers.Models.Surveys;
using Microsoft.EntityFrameworkCore;

namespace Answers.Data.Repositories;

public class SurveyRepository : Repository<Survey>, ISurveyRepository
{
    public SurveyRepository(DbContext dbContext) : base(dbContext)
    {
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