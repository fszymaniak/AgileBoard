using AgileBoard.Application;
using AgileBoard.Core;
using AgileBoard.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddControllers();

// TODO: Add authentication when ready for production
// Example:
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options => {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//             // Configure your token validation parameters
//         };
//     });
// builder.Services.AddAuthorization();

var app = builder.Build();

// TODO: Enable authentication/authorization middleware when configured
// app.UseAuthentication();
// app.UseAuthorization();

app.UseInfrastructure();
app.Run();
