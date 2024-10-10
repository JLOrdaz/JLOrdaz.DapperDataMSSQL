namespace JLOrdaz.DapperDataMSSQL;


/// <summary>
/// Provides methods for interacting with a SQL database using stored procedures.
/// </summary>
public interface ISQLDataAccess
{
    /// <summary>
    /// Loads data from the database using a stored procedure.
    /// </summary>
    /// <typeparam name="T">The type of the objects to be returned.</typeparam>
    /// <typeparam name="U">The type of the parameters to be passed to the stored procedure.</typeparam>
    /// <param name="storeProcedure">The name of the stored procedure to execute.</param>
    /// <param name="parameters">The parameters to pass to the stored procedure.</param>
    /// <param name="connectionString">The connection string.</param>
    /// <returns>A task representing the asynchronous operation, containing an enumerable of the specified type.</returns>
    Task<IEnumerable<T>> LoadData<T, U>(string storeProcedure, U parameters, string connectionString);

    /// <summary>
    /// Executes a stored procedure and returns the first result as an object of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the object to be returned.</typeparam>
    /// <typeparam name="U">The type of the parameters to be passed to the stored procedure.</typeparam>
    /// <param name="storeProcedure">The name of the stored procedure to execute.</param>
    /// <param name="parameters">The parameters to pass to the stored procedure.</param>
    /// <param name="connectionString">The connection string.</param>
    /// <returns>An object of the specified type containing the first result of the stored procedure execution.</returns>
    /// <remarks>If the stored procedure returns no results, the method returns null.</remarks>
    /// <remarks>If the stored procedure returns multiple results, the method returns the first one.</remarks>
    Task<T?> LoadFirst<T, U>(string storeProcedure, U parameters, string connectionString);

    /// <summary>
    /// Executes a stored procedure that does not return any results.
    /// </summary>
    /// <typeparam name="T">The type of the parameters to be passed to the stored procedure.</typeparam>
    /// <param name="storeProcedure">The name of the stored procedure to execute.</param>
    /// <param name="parameters">The parameters to pass to the stored procedure.</param>
    /// <param name="connectionString">The connection string identifier.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>This method is useful for stored procedures that perform insert, update, or delete operations.</remarks>
    Task SaveData<T>(string storeProcedure, T parameters, string connectionString);

}