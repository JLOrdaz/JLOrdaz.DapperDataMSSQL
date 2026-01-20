using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace JLOrdaz.DapperDataMSSQL;

/// <summary>
/// Provides data access helpers to execute stored procedures against SQL Server using Dapper.
/// </summary>
/// <remarks>This class is intended to be used with Dapper and Microsoft.Data.SqlClient.</remarks>
public class SQLDataAccess(IConfiguration config) : ISQLDataAccess
{
    /// <summary>
    /// Executes a stored procedure and returns all rows mapped to the specified type.
    /// </summary>
    /// <typeparam name="T">The result model type to map each row to.</typeparam>
    /// <typeparam name="U">The type of the parameters object passed to the stored procedure.</typeparam>
    /// <param name="storeProcedure">The stored procedure name to execute.</param>
    /// <param name="parameters">The parameters object to pass to the stored procedure.</param>
    /// <param name="connectionString">The name of the connection string configured in <c>IConfiguration</c>.</param>
    /// <returns>An enumerable of <typeparamref name="T"/> with the results. Returns an empty collection when no rows are returned.</returns>
    public async Task<IEnumerable<T>> LoadData<T, U>(string storeProcedure, U parameters, string connectionString)
    {
        using IDbConnection conex = new SqlConnection(config.GetConnectionString(connectionString));
        return await conex.QueryAsync<T>(storeProcedure, parameters, commandType: CommandType.StoredProcedure) ?? [];
    }

    /// <summary>
    /// Executes a stored procedure and returns the first row mapped to the specified type.
    /// </summary>
    /// <typeparam name="T">The result model type to map to.</typeparam>
    /// <typeparam name="U">The type of the parameters object passed to the stored procedure.</typeparam>
    /// <param name="storeProcedure">The stored procedure name to execute.</param>
    /// <param name="parameters">The parameters object to pass to the stored procedure.</param>
    /// <param name="connectionString">The name of the connection string configured in <c>IConfiguration</c>.</param>
    /// <returns>The first result mapped to <typeparamref name="T"/>, or <see langword="null"/> if no rows are returned.</returns>
    public async Task<T?> LoadFirst<T, U>(string storeProcedure, U parameters, string connectionString)
    {
        using IDbConnection conex = new SqlConnection(config.GetConnectionString(connectionString));
        return await conex.QueryFirstOrDefaultAsync<T?>(storeProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Executes a stored procedure that does not return any results.
    /// </summary>
    /// <typeparam name="T">The type of the parameters object passed to the stored procedure.</typeparam>
    /// <param name="storeProcedure">The stored procedure name to execute.</param>
    /// <param name="parameters">The parameters object to pass to the stored procedure.</param>
    /// <param name="connectionString">The name of the connection string configured in <c>IConfiguration</c>.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SaveData<T>(string storeProcedure, T parameters, string connectionString)
    {
        using IDbConnection conex = new SqlConnection(config.GetConnectionString(connectionString));
        await conex.ExecuteAsync(storeProcedure, parameters, commandType: CommandType.StoredProcedure);
    }
}
