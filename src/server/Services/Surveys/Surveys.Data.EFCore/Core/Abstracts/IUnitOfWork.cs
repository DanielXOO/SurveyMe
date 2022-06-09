namespace Surveys.Data.Core.Abstracts;

public interface IUnitOfWork
{
    IRepository<T> GetRepository<T>() where T : class;
}