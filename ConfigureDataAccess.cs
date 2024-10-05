using JLOrdaz.DapperDataMSSQL;
using Microsoft.Extensions.DependencyInjection;


public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDapperDataMSSQL(this IServiceCollection services)
    {
        services.AddScoped<ISQLDataAccess, SQLDataAccess>();
        return services;
    }
}
