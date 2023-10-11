using System.Runtime.CompilerServices;
using AgileBoard.Application.Services.Clock;
using AgileBoard.Infrastructure.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("AgileBoard.Tests.Unit")]
namespace AgileBoard.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddPostgres(configuration)
            .AddSingleton<IClock, Clock.Clock>();
            
        return services;
    }
}