using AgileBoard.Api.Repositories;
using AgileBoard.Api.Services.Clock;
using AgileBoard.Api.Services.EpicsService;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<IClock, Clock>()
    .AddSingleton<IEpicsService, EpicsService>()
    .AddSingleton<IEpicRepository, InMemoryEpicRepository>()
    .AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
