using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace SurveyPerson.Data.Dapper;

public static class DapperExtension
{
    public static void AddDapperConnection(this IServiceCollection services, string connectionString)
    {
        services.AddScoped( _ => new SqlConnection(connectionString));
        services.AddScoped<IDbTransaction>(provider =>
        {
            var connection = provider.GetRequiredService<SqlConnection>();
            connection.Open();

            var transaction =  connection.BeginTransaction();

            return transaction;
        });
    }
}