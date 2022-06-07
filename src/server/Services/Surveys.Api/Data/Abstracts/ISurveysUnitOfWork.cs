using Surveys.Api.Data.Core.Abstracts;
using Surveys.Api.Data.Repositories.Abstracts;

namespace Surveys.Api.Data.Abstracts;

public interface ISurveysUnitOfWork : IUnitOfWork
{
    ISurveysRepository Surveys { get; }
}