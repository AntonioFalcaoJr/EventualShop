using System.Net;
using System.Net.Mail;
using Application.Abstractions.Gateways;
using Domain.ValueObject;
using Infrastructure.SMTP.DependencyInjection.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.SMTP.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddEmailGateway(this IServiceCollection services)
        => services.AddScoped<INotificationOptionGateway<Email>, EmailGateway>();

    public static void AddSmtpClient(this IServiceCollection services)
        => services.AddScoped<SmtpClient>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<SmtpOptions>>().Value;
            return new(options.Host, options.Port)
            {
                Credentials = new NetworkCredential(options.Username, options.Password),
                EnableSsl = options.EnableSsl,
            };
        });

    public static OptionsBuilder<SmtpOptions> ConfigureSmtpOptions(this IServiceCollection services, IConfigurationSection section)
        => services
            .AddOptions<SmtpOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
}