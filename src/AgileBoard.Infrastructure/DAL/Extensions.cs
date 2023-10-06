using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AgileBoard.Infrastructure.DAL;

internal static class Extensions
{
    public static IServiceCollection AddPostgres(this IServiceCollection services)
    {
        const string connectionString = "Host=localhost;Database=AgileBoard;Username=postgres;Password=";
        services.AddDbContext<AgileBoardDbContext>(x => x.UseNpgsql(connectionString));

        return services;
    }
}