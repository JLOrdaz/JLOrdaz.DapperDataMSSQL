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
    /// <returns>The same <see cref="IServiceCollection"/> instance so that additional calls can be chained.</returns>
    public static IServiceCollection AddDapperDataMSSQL(this IServiceCollection services)
    {
        return services.AddScoped<ISQLDataAccess, SQLDataAccess>();
    }
}
