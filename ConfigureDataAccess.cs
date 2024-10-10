using Microsoft.Extensions.DependencyInjection;

namespace JLOrdaz.DapperDataMSSQL;

/// <summary>
/// Provides extension methods for configuring data access services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the DapperDataMSSQL data access services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    public static IServiceCollection AddDapperDataMSSQL(this IServiceCollection services)
    {
        return services.AddScoped<ISQLDataAccess, SQLDataAccess>();
    }
}
