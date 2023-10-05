using AgileBoard.Application.Services.EpicsService;
using Microsoft.Extensions.DependencyInjection;

namespace AgileBoard.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IEpicsService, EpicsService>();

        return services;
    }
}