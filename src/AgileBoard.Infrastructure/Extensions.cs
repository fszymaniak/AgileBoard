using System.Runtime.CompilerServices;
using AgileBoard.Application.Services.Clock;
using AgileBoard.Core.Repositories;
using AgileBoard.Infrastructure.DAL;
using AgileBoard.Infrastructure.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("AgileBoard.Tests.Unit")]
namespace AgileBoard.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddPostgres()
            .AddSingleton<IClock, Clock.Clock>();
            
        return services;
    }
}