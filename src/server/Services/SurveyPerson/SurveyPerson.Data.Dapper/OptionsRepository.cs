using System.Data;
using System.Data.SqlClient;
using Dapper;
using SurveyPerson.Data.Dapper.Abstracts;
using SurveyPerson.Models.Options;

namespace SurveyPerson.Data.Dapper;

public sealed class OptionsRepository : IOptionsRepository
{
    private readonly IDbTransaction _transaction;

    private readonly SqlConnection _connection;
    
    
    public OptionsRepository(SqlConnection connection, IDbTransaction transaction)
    {
        _connection = connection;
        _transaction = transaction;
    }
    
    
    public async Task CreateAsync(SurveyPersonOptions options)
    {
        const string sql = "EXEC AddNewOption @Id, @SurveyId, @RequireFirstName," +
                           " @RequireSecondName, @RequireAges, @RequireGender";

        await _connection.ExecuteAsync(sql, new
        {
            Id = options.Id, 
            SurveyId = options.SurveyId, 
            RequireFirstName = options.RequireFirstName, 
            RequireSecondName = options.RequireSecondName, 
            RequireAges = options.RequireAges, 
            RequireGender = options.RequireGender
        }, _transaction);
    }

    public async Task<SurveyPersonOptions> GetByIdAsync(Guid id)
    {
        const string sql = "SELECT * FROM GetOptionsById(@Id)";

        var options = await _connection.QuerySingleOrDefaultAsync(sql, new
        {
            Id = id
        }, _transaction);

        return options;
    }

    public async Task<SurveyPersonOptions> GetBySurveyIdAsync(Guid surveyId)
    {
        const string sql = "SELECT * FROM GetOptionsBySurveyId(@SurveyId)";

        var options = await _connection
            .QueryFirstOrDefaultAsync<SurveyPersonOptions>(sql, new
            {
                SurveyId = surveyId
            }, _transaction);

        return options;
    }

    public async Task UpdateAsync(SurveyPersonOptions options)
    {
        const string sql = "EXEC UpdateOption @Id, @SurveyId, " +
                           "@RequireFirstName, @RequireSecondName, " +
                           "@RequireAges, @RequireGender";

        await _connection.ExecuteAsync(sql, new
        {
            Id = options.Id,
            SurveyId = options.SurveyId,
            RequireFirstName = options.RequireFirstName,
            RequireSecondName = options.RequireSecondName,
            RequireAges = options.RequireAges,
            RequireGender = options.RequireGender
        }, _transaction);
    }

    public async Task DeleteAsync(Guid id)
    {
        const string sql = "EXEC DeleteOption @Id";

        await _connection.ExecuteAsync(sql, new
        {
            Id = id
        }, _transaction);
    }
}