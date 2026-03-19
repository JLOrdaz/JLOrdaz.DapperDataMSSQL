using Microsoft.Extensions.DependencyInjection;

namespace JLOrdaz.DapperDataMSSQL;

/// <summary>
/// Provides extension methods for configuring data access services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the Dapper-based SQL Server data access services.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="useConnectionStringName">If <c>true</c>, the <paramref name="connectionStringOrName"/> is treated as a configuration key name. If <c>false</c>, it's treated as a direct connection string.</param>
    /// <param name="connectionStringOrName">The connection string configuration key name or the direct connection string value.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance so that additional calls can be chained.</returns>
    public static IServiceCollection AddDapperDataMSSQL(this IServiceCollection services, bool useConnectionStringName, string connectionStringOrName)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionStringOrName);

        return services.AddScoped<ISQLDataAccess>(provider =>
            new SQLDataAccess(provider.GetRequiredService<Microsoft.Extensions.Configuration.IConfiguration>(), useConnectionStringName, connectionStringOrName));
    }
}
