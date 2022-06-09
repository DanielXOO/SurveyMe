using Answers.Data.Abstracts;
using Answers.Data.Core;
using Answers.Data.Repositories;
using Answers.Data.Repositories.Abstracts;
using Answers.Models.Answers;

namespace Answers.Data;

public class AnswersUnitOfWork : UnitOfWork, IAnswersUnitOfWork
{
    public IAnswersRepository Answers
        => (IAnswersRepository)GetRepository<SurveyAnswer>(); 
    
    public AnswersUnitOfWork(AnswersDbContext dbContext)
        : base(dbContext)
    {
        AddSpecificRepository<SurveyAnswer, AnswersRepository>();
    }
}