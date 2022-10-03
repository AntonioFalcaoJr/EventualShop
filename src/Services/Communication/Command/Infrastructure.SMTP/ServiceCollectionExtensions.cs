using Application.Abstractions.Gateways;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.SMTP;

public static class ServiceCollectionExtensions
{
    public static void AddSmtp(this IServiceCollection services)
        => services.AddScoped<IEmailGateway, EmailGateway>();
}