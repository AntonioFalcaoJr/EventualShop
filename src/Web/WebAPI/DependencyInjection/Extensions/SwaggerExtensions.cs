using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebAPI.DependencyInjection.Options;

namespace WebAPI.DependencyInjection.Extensions;

public static class SwaggerExtensions
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
    }
}