using AgileBoard.Api.Services.Clock;
using AgileBoard.Api.Services.EpicsService;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<IClock, Clock>()
    .AddSingleton<IEpicsService, EpicsService>()
    .AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
