using System.Data;
using SurveyPerson.Data.Dapper.Abstracts;

namespace SurveyPerson.Data.Dapper;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly IDbTransaction _transaction;


    public IOptionsRepository Options { get; }
    
    
    public UnitOfWork(IDbTransaction transaction, IOptionsRepository options)
    {
        _transaction = transaction;
        Options = options;
    }
    

    public void Commit()
    {
        try
        {
            _transaction.Commit();
        }
        catch (Exception e)
        {
            //TODO: Log it
            _transaction.Rollback();
        }
    }

    public void Dispose()
    {
        _transaction.Connection?.Close();
        _transaction.Connection?.Dispose();
        _transaction.Dispose();
    }
}