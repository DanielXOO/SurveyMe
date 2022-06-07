using Surveys.Api.Data.Abstracts;
using Surveys.Api.Data.Core;
using Surveys.Api.Data.Repositories;
using Surveys.Api.Data.Repositories.Abstracts;
using Surveys.Api.Models.Surveys;

namespace Surveys.Api.Data;

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