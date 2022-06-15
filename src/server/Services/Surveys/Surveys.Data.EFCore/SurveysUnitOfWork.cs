using Surveys.Data.Abstracts;
using Surveys.Data.Core;
using Surveys.Data.Repositories;
using Surveys.Data.Repositories.Abstracts;
using Surveys.Models.Surveys;

namespace Surveys.Data;

public class SurveysUnitOfWork : UnitOfWork, ISurveysUnitOfWork
{
    public ISurveysRepository Surveys
        => (ISurveysRepository) GetRepository<Survey>();


    public SurveysUnitOfWork(SurveysDbContext dbContext)
        : base(dbContext)
    {
        AddSpecificRepository<Survey, SurveysRepository>();
    }
}