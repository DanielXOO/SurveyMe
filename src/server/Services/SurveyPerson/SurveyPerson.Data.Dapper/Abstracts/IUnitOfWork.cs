namespace SurveyPerson.Data.Dapper.Abstracts;

public interface IUnitOfWork
{
    IOptionsRepository Options { get; }

    void Commit();
}