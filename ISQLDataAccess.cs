namespace JLOrdaz.DapperDataMSSQL;

/// <summary>
/// Provides methods for interacting with a SQL Server database via stored procedures using Dapper.
/// </summary>
public interface ISQLDataAccess
{
    /// <summary>
    /// Executes a stored procedure and returns all rows mapped to the specified type.
    /// </summary>
    /// <typeparam name="T">The type to map result rows to.</typeparam>
    /// <typeparam name="U">The type of the parameters object passed to the stored procedure.</typeparam>
    /// <param name="storeProcedure">The stored procedure name to execute.</param>
    /// <param name="parameters">The parameters object to pass to the stored procedure.</param>
    /// <param name="connectionString">The name of the connection string configured in <c>IConfiguration</c>.</param>
    /// <returns>A task that returns an enumerable of <typeparamref name="T"/>. Returns an empty collection when no rows are returned.</returns>
    Task<IEnumerable<T>> LoadData<T, U>(string storeProcedure, U parameters, string connectionString);

    /// <summary>
    /// Executes a stored procedure and returns the first row mapped to the specified type.
    /// </summary>
    /// <typeparam name="T">The type to map the first result row to.</typeparam>
    /// <typeparam name="U">The type of the parameters object passed to the stored procedure.</typeparam>
    /// <param name="storeProcedure">The stored procedure name to execute.</param>
    /// <param name="parameters">The parameters object to pass to the stored procedure.</param>
    /// <param name="connectionString">The name of the connection string configured in <c>IConfiguration</c>.</param>
    /// <returns>A task that returns the first result mapped to <typeparamref name="T"/>, or <see langword="null"/> if no rows are returned.</returns>
    Task<T?> LoadFirst<T, U>(string storeProcedure, U parameters, string connectionString);

    /// <summary>
    /// Executes a stored procedure that does not return any results.
    /// </summary>
    /// <typeparam name="T">The type of the parameters object passed to the stored procedure.</typeparam>
    /// <param name="storeProcedure">The stored procedure name to execute.</param>
    /// <param name="parameters">The parameters object to pass to the stored procedure.</param>
    /// <param name="connectionString">The name of the connection string configured in <c>IConfiguration</c>.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>Use for insert, update, or delete operations that do not return results.</remarks>
    Task SaveData<T>(string storeProcedure, T parameters, string connectionString);
}