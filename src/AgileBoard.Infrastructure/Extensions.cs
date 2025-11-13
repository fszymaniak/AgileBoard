using System.Runtime.CompilerServices;
using AgileBoard.Application.Services.Clock;
using AgileBoard.Application.Services.UserContext;
using AgileBoard.Infrastructure.DAL;
using AgileBoard.Infrastructure.Exceptions;
using AgileBoard.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("AgileBoard.Tests.Unit")]
namespace AgileBoard.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ExceptionMiddleware>();

        services
            .AddPostgres(configuration)
            .AddSingleton<IClock, Clock.Clock>();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUserContext, UserContext>();

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        app.MapControllers();

        return app;
    }
}