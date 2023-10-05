using AgileBoard.Application;
using AgileBoard.Core;
using AgileBoard.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure()
    .AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
