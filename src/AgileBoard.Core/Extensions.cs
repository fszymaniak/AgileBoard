using AgileBoard.Core.DomainServices;
using AgileBoard.Core.Policies;
using AgileBoard.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AgileBoard.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSingleton<IEpicPolicy, DevelopmentTeamMemberEpicCreationPolicy>();
        services.AddSingleton<IEpicPolicy, ProductOwnerEpicCreationPolicy>();
        services.AddSingleton<IEpicPolicy, ScrumMasterEpicCreationPolicy>();
        services.AddSingleton<IEpicCreationService, EpicCreationService>();
        return services;
    }
}