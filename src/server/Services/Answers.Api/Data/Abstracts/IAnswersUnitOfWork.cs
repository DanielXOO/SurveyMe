using Answers.Api.Data.Core.Abstracts;
using Answers.Api.Data.Repositories.Abstracts;

namespace Answers.Api.Data.Abstracts;

public interface IAnswersUnitOfWork : IUnitOfWork
{
    public IAnswersRepository Answers { get; }
}