using AgileBoard.Core.DomainServices;
using AgileBoard.Core.DomainServices.Creation;
using AgileBoard.Core.Policies;
using AgileBoard.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AgileBoard.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSingleton<IEpicPolicy, DevelopmentTeamMemberFinalEpicCreationPolicy>();
        services.AddSingleton<IEpicPolicy, ProductOwnerFinalEpicCreationPolicy>();
        services.AddSingleton<IEpicPolicy, ScrumMasterFinalEpicCreationPolicy>();
        services.AddSingleton<IEpicCreationService, EpicCreationService>();
        return services;
    }
}