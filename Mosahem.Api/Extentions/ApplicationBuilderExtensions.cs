using Microsoft.Extensions.Options;
using mosahem.Presentation.Middleware;

namespace mosahem.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static WebApplication UseApiPipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "mosahem API v1");
                    options.RoutePrefix = "swagger";
                    options.DisplayRequestDuration();
                    options.EnableFilter();
                });
            }

            // Localization
            var localizationOptions =
                app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizationOptions.Value);

            // Global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors(ServiceCollectionExtensions.CorsPolicyName);

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
