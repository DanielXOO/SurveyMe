using Answers.Data.Core.Abstracts;
using Answers.Data.Repositories.Abstracts;

namespace Answers.Data.Abstracts;

public interface IAnswersUnitOfWork : IUnitOfWork
{
    public IAnswersRepository Answers { get; }
}