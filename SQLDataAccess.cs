using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace JLOrdaz.DapperDataMSSQL;

public class SQLDataAccess(IConfiguration configuration) : ISQLDataAccess
{
    public async Task<IEnumerable<T>> LoadData<T, U>(string storeProcedure, U parameters, string connectionId = "BD")
    {
        using IDbConnection conex = new SqlConnection(configuration.GetConnectionString(connectionId));
        return await conex.QueryAsync<T>(storeProcedure, parameters, commandType: CommandType.StoredProcedure) ?? Enumerable.Empty<T>();
    }

    public async Task<T?> LoadFirst<T, U>(string storeProcedure, U parameters, string connectionId = "BD")
    {
        using IDbConnection conex = new SqlConnection(configuration.GetConnectionString(connectionId));
        return await conex.QueryFirstOrDefaultAsync<T>(storeProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task SaveData<T>(string storeProcedure, T parameters, string connectionId = "BD")
    {
        using IDbConnection conex = new SqlConnection(configuration.GetConnectionString(connectionId));
        await conex.ExecuteAsync(storeProcedure, parameters, commandType: CommandType.StoredProcedure);
    }
}
