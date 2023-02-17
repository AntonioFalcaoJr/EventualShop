using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using WebAPI.DependencyInjection.Options;

namespace WebAPI.DependencyInjection.Extensions;

public static class SwaggerExtensions
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
    }
    
    public static void ConfigureSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var version in app.DescribeApiVersions().Select(version => version.GroupName))
                options.SwaggerEndpoint($"/swagger/{version}/swagger.json", version);

            options.EnableTryItOutByDefault();
            options.DocExpansion(DocExpansion.None);
            options.DisplayRequestDuration();

            app.MapGet("/", () => Results.Redirect("/swagger/index.html"))
                .WithTags(string.Empty);
        });
    }
}