using Surveys.Data.Core.Abstracts;
using Surveys.Data.Repositories.Abstracts;

namespace Surveys.Data.Abstracts;

public interface ISurveysUnitOfWork : IUnitOfWork
{
    ISurveysRepository Surveys { get; }
}