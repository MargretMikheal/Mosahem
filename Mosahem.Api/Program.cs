using mosahem.Api.Extensions;
using mosahem.Application;
using mosahem.Infrastructure;
using mosahem.Persistence;
using mosahem.Persistence.Seeds;
using mosahem.Presistence;
using Mosahem.Application.Interfaces.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiServices()
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddAuthenticationServices(builder.Configuration)
    .AddPersistenceServices(builder.Configuration)
    .AddSwaggerDocumentation();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<MosahmDbContext>();
        var passwordHasher = services.GetRequiredService<IPasswordHasher>();

        await ContextSeed.SeedAsync(context, passwordHasher);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.UseApiPipeline();

app.Run();
