using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace JLOrdaz.DapperDataMSSQL;

/// <summary>
/// Provides data access helpers to execute stored procedures against SQL Server using Dapper.
/// </summary>
/// <remarks>This class is intended to be used with Dapper and Microsoft.Data.SqlClient.</remarks>
public class SQLDataAccess : ISQLDataAccess
{
    private readonly IConfiguration _config;
    private readonly bool _useConnectionStringName;
    private readonly string _connectionStringOrName;

    /// <summary>
    /// Creates a new SQL data access helper.
    /// </summary>
    /// <param name="config">Application configuration used to resolve named connection strings.</param>
    /// <param name="useConnectionStringName">Whether <paramref name="connectionStringOrName"/> is a configuration key.</param>
    /// <param name="connectionStringOrName">The connection string or the connection string name.</param>
    public SQLDataAccess(IConfiguration config, bool useConnectionStringName, string connectionStringOrName)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionStringOrName);

        _config = config;
        _useConnectionStringName = useConnectionStringName;
        _connectionStringOrName = connectionStringOrName;
    }

    private string GetConnectionString()
    {
        if (_useConnectionStringName)
        {
            return _config.GetConnectionString(_connectionStringOrName) ?? throw new InvalidOperationException($"Connection string '{_connectionStringOrName}' not found in configuration.");
        }
        else
        {
            return _connectionStringOrName;
        }
    }

    /// <summary>
    /// Executes a stored procedure and returns all rows mapped to the specified type.
    /// </summary>
    /// <typeparam name="T">The result model type to map each row to.</typeparam>
    /// <typeparam name="U">The type of the parameters object passed to the stored procedure.</typeparam>
    /// <param name="storeProcedure">The stored procedure name to execute.</param>
    /// <param name="parameters">The parameters object to pass to the stored procedure.</param>
    /// <returns>An enumerable of <typeparamref name="T"/> with the results. Returns an empty collection when no rows are returned.</returns>
    public async Task<IEnumerable<T>> LoadData<T, U>(string storeProcedure, U parameters)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(storeProcedure);

        using IDbConnection conex = new SqlConnection(GetConnectionString());
        var result = await conex.QueryAsync<T>(storeProcedure, parameters, commandType: CommandType.StoredProcedure);
        return result ?? Enumerable.Empty<T>();
    }

    /// <summary>
    /// Executes a stored procedure and returns the first row mapped to the specified type.
    /// </summary>
    /// <typeparam name="T">The result model type to map to.</typeparam>
    /// <typeparam name="U">The type of the parameters object passed to the stored procedure.</typeparam>
    /// <param name="storeProcedure">The stored procedure name to execute.</param>
    /// <param name="parameters">The parameters object to pass to the stored procedure.</param>
    /// <returns>The first result mapped to <typeparamref name="T"/>, or <see langword="null"/> if no rows are returned.</returns>
    public async Task<T?> LoadFirst<T, U>(string storeProcedure, U parameters)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(storeProcedure);

        using IDbConnection conex = new SqlConnection(GetConnectionString());
        return await conex.QueryFirstOrDefaultAsync<T?>(storeProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Executes a stored procedure that does not return any results.
    /// </summary>
    /// <typeparam name="T">The type of the parameters object passed to the stored procedure.</typeparam>
    /// <param name="storeProcedure">The stored procedure name to execute.</param>
    /// <param name="parameters">The parameters object to pass to the stored procedure.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SaveData<T>(string storeProcedure, T parameters)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(storeProcedure);

        using IDbConnection conex = new SqlConnection(GetConnectionString());
        await conex.ExecuteAsync(storeProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Executes a stored procedure that returns multiple result sets and maps them to two different types.
    /// </summary>
    public async Task<(IEnumerable<T1>, IEnumerable<T2>)> LoadMultiple<T1, T2, U>(string storeProcedure, U parameters)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(storeProcedure);

        using IDbConnection conex = new SqlConnection(GetConnectionString());
        using var multi = await conex.QueryMultipleAsync(storeProcedure, parameters, commandType: CommandType.StoredProcedure);
        var first = await multi.ReadAsync<T1>();
        var second = await multi.ReadAsync<T2>();
        return (first, second);
    }

    /// <summary>
    /// Executes a stored procedure and returns a single scalar value mapped to the specified type.
    /// </summary>
    public async Task<T?> ExecuteScalarAsync<T, U>(string storeProcedure, U parameters)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(storeProcedure);

        using IDbConnection conex = new SqlConnection(GetConnectionString());
        var result = await conex.ExecuteScalarAsync(storeProcedure, parameters, commandType: CommandType.StoredProcedure);
        return ConvertScalar<T>(result);
    }

    private static T? ConvertScalar<T>(object? result)
    {
        if (result is null || result is DBNull)
        {
            return default;
        }

        if (result is T value)
        {
            return value;
        }

        var targetType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

        if (targetType.IsEnum)
        {
            return (T)Enum.ToObject(targetType, result);
        }

        return (T)Convert.ChangeType(result, targetType, CultureInfo.InvariantCulture);
    }
}
