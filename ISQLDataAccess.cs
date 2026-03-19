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
    /// <returns>A task that returns an enumerable of <typeparamref name="T"/>. Returns an empty collection when no rows are returned.</returns>
    Task<IEnumerable<T>> LoadData<T, U>(string storeProcedure, U parameters);

    /// <summary>
    /// Executes a stored procedure and returns the first row mapped to the specified type.
    /// </summary>
    /// <typeparam name="T">The type to map the first result row to.</typeparam>
    /// <typeparam name="U">The type of the parameters object passed to the stored procedure.</typeparam>
    /// <param name="storeProcedure">The stored procedure name to execute.</param>
    /// <param name="parameters">The parameters object to pass to the stored procedure.</param>
    /// <returns>A task that returns the first result mapped to <typeparamref name="T"/>, or <see langword="null"/> if no rows are returned.</returns>
    Task<T?> LoadFirst<T, U>(string storeProcedure, U parameters);

    /// <summary>
    /// Executes a stored procedure that does not return any results.
    /// </summary>
    /// <typeparam name="T">The type of the parameters object passed to the stored procedure.</typeparam>
    /// <param name="storeProcedure">The stored procedure name to execute.</param>
    /// <param name="parameters">The parameters object to pass to the stored procedure.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>Use for insert, update, or delete operations that do not return results.</remarks>
    Task SaveData<T>(string storeProcedure, T parameters);

    /// <summary>
    /// Executes a stored procedure that returns multiple result sets and maps them to two different types.
    /// </summary>
    /// <typeparam name="T1">The type to map the first result set to.</typeparam>
    /// <typeparam name="T2">The type to map the second result set to.</typeparam>
    /// <typeparam name="U">The type of the parameters object passed to the stored procedure.</typeparam>
    /// <param name="storeProcedure">The stored procedure name to execute.</param>
    /// <param name="parameters">The parameters object to pass to the stored procedure.</param>
    /// <returns>
    /// A task that returns a tuple where Item1 is an enumerable of <typeparamref name="T1"/> and
    /// Item2 is an enumerable of <typeparamref name="T2"/>. Empty enumerables are returned when result sets are empty.
    /// </returns>
    Task<(IEnumerable<T1>, IEnumerable<T2>)> LoadMultiple<T1, T2, U>(string storeProcedure, U parameters);

    /// <summary>
    /// Executes a stored procedure and returns a single scalar value mapped to the specified type.
    /// </summary>
    /// <typeparam name="T">The type to map the scalar result to.</typeparam>
    /// <typeparam name="U">The type of the parameters object passed to the stored procedure.</typeparam>
    /// <param name="storeProcedure">The stored procedure name to execute.</param>
    /// <param name="parameters">The parameters object to pass to the stored procedure.</param>
    /// <returns>The scalar result mapped to <typeparamref name="T"/>, or <see langword="null"/> when no value is returned.</returns>
    Task<T?> ExecuteScalarAsync<T, U>(string storeProcedure, U parameters);
}