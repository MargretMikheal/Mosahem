using mosahem.Api.Extensions;
using mosahem.Application;
using mosahem.Infrastructure;
using mosahem.Presistence;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddApiServices()
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddAuthenticationServices(builder.Configuration)
    .AddPersistenceServices(builder.Configuration)
    .AddSwaggerDocumentation();

var app = builder.Build();


app.UseApiPipeline();

app.Run();
