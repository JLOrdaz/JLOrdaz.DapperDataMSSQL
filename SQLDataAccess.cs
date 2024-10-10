using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace JLOrdaz.DapperDataMSSQL;

/// <summary>
/// Represents a data access object that interacts with a SQL database.
/// </summary>
/// <remarks>This interface is intended to be used with Dapper and Microsoft.Data.SqlClient.</remarks>
/// 
public class SQLDataAccess : ISQLDataAccess
{
    /// <summary>
    /// Executes a stored procedure and returns the result as an enumerable collection of the specified type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="storeProcedure"></param>
    /// <param name="parameters"></param>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    public async Task<IEnumerable<T>> LoadData<T, U>(string storeProcedure, U parameters, string connectionString)
    {
        using IDbConnection conex = new SqlConnection(connectionString);
        return await conex.QueryAsync<T>(storeProcedure, parameters, commandType: CommandType.StoredProcedure) ?? [];
    }

    /// <summary>
    /// Executes a stored procedure and returns the first result as an object of the specified type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="storeProcedure"></param>
    /// <param name="parameters"></param>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    public async Task<T?> LoadFirst<T, U>(string storeProcedure, U parameters, string connectionString)
    {
        using IDbConnection conex = new SqlConnection(connectionString);
        return await conex.QueryFirstOrDefaultAsync<T>(storeProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Executes a stored procedure that does not return any results.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="storeProcedure"></param>
    /// <param name="parameters"></param>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    /// <remarks>This method is useful for stored procedures that perform insert, update, or delete operations.</remarks>
    public async Task SaveData<T>(string storeProcedure, T parameters, string connectionString)
    {
        using IDbConnection conex = new SqlConnection(connectionString);
        await conex.ExecuteAsync(storeProcedure, parameters, commandType: CommandType.StoredProcedure);
    }
}
