using Application.Abstractions.Gateways;
using Infrastructure.SMTP.DependencyInjection.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.SMTP.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddSmtp(this IServiceCollection services)
        => services.AddScoped<IEmailGateway, EmailGateway>();

    public static OptionsBuilder<SmtpOptions> ConfigureSmtpOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<SmtpOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
}