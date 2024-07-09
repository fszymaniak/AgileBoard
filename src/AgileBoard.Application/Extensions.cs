using AgileBoard.Application.Services.DraftEpicsService;
using AgileBoard.Application.Services.FinalEpicsService;
using Microsoft.Extensions.DependencyInjection;

namespace AgileBoard.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IFinalEpicsService, FinalFinalEpicsService>();
        services.AddScoped<IDraftEpicsService, DraftFinalEpicsService>();

        return services;
    }
}