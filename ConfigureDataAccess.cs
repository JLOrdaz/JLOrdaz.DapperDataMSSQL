using JLOrdaz.DapperDataMSSQL;
using Microsoft.Extensions.DependencyInjection;

namespace JLOrdaz.DapperDataMSSQL;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDapperDataMSSQL(this IServiceCollection services)
    {
        return services.AddScoped<ISQLDataAccess, SQLDataAccess>();
    }
}
