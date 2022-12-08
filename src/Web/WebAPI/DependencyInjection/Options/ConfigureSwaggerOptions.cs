using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebAPI.DependencyInjection.Options;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
            options.SwaggerDoc(description.GroupName, new()
            {
                Title = AppDomain.CurrentDomain.FriendlyName,
                Version = description.ApiVersion.ToString()
            });
        
        options.MapType<DateOnly>(() => new()
        {
            Format = "date", 
            Example = new OpenApiString(DateOnly.MinValue.ToString())
        });
        
        options.CustomSchemaIds(type => type.ToString().Replace("+", "."));
    }
}