namespace SurveyMe.Data.Contracts;

public interface IUnitOfWork
{
    IRepository<T> GetRepository<T>() where T : class;
}