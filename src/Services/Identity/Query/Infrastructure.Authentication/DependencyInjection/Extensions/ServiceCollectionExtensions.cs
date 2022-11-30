using Application.Abstractions;
using Infrastructure.Authentication.DependencyInjection.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Authentication.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection services)
        => services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

    public static OptionsBuilder<JwtOptions> ConfigureJwtOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<JwtOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
}