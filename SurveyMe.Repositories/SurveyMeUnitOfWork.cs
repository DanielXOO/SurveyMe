using Microsoft.EntityFrameworkCore;
using SurveyMe.Common.Repositories.EFCore;

namespace SurveyMe.Repositories;

public class SurveyMeUnitOfWork : UnitOfWork, ISurveyMeUnitOfWork
{
    public SurveyMeUnitOfWork(DbContext dbContext) : base(dbContext)
    {
    }
}