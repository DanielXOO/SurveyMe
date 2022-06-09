using Answers.Api.Data.Abstracts;
using Answers.Api.Data.Core;
using Answers.Api.Data.Repositories.Abstracts;
using Answers.Api.Models.Answers;

namespace Answers.Api.Data;

public class AnswersUnitOfWork : UnitOfWork, IAnswersUnitOfWork
{
    public IAnswersRepository Answers
        => (IAnswersRepository)GetRepository<SurveyAnswer>(); 
    
    public AnswersUnitOfWork(AnswersDbContext dbContext)
        : base(dbContext)
    {
        AddSpecificRepository<SurveyAnswer, IAnswersRepository>();
    }
}