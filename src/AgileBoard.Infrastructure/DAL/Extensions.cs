using AgileBoard.Core.Repositories;
using AgileBoard.Infrastructure.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AgileBoard.Infrastructure.DAL;

internal static class Extensions
{
    public static IServiceCollection AddPostgres(this IServiceCollection services)
    {
        const string connectionString = "Host=localhost;Database=AgileBoard;Username=postgres;Password=";
        services.AddDbContext<AgileBoardDbContext>(x => x.UseNpgsql(connectionString));
        services.AddScoped<IEpicRepository, PostgresEpicRepository>();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    }
}