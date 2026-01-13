using Mosahm.Api.Extensions;
using Mosahm.Application;
using Mosahm.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddApiServices()
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddAuthenticationServices(builder.Configuration)
    .AddSwaggerDocumentation();

var app = builder.Build();


app.UseApiPipeline();

app.Run();
