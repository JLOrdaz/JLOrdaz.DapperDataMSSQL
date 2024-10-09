namespace JLOrdaz.DapperDataMSSQL;

public interface ISQLDataAccess
{
    /// <summary>
    /// Executes a stored procedure and returns the result as an enumerable collection of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the objects to be returned.</typeparam>
    /// <typeparam name="U">The type of the parameters to be passed to the stored procedure.</typeparam>
    /// <param name="storeProcedure">The name of the stored procedure to execute.</param>
    /// <param name="parameters">The parameters to pass to the stored procedure.</param>
    /// <param name="connectionId">The connection string identifier. Defaults to "DB".</param>
    /// <returns>An enumerable collection of the specified type containing the results of the stored procedure execution.</returns>
    Task<IEnumerable<T>> LoadData<T, U>(string storeProcedure, U parameters, string connectionId = "DB");
    Task<T?> LoadFirst<T, U>(string storeProcedure, U parameters, string connectionId = "DB");
    Task SaveData<T>(string storeProcedure, T parameters, string connectionId = "DB");
}
