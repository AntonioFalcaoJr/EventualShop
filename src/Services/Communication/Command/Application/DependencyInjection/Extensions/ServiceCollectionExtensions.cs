using Application.Abstractions.Gateways;
using Application.Services;
using Application.UseCases.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services
            .AddScoped<IApplicationService, ApplicationService>()
            .AddScoped<INotificationService, NotificationService>();

    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services
            .AddScoped<IRequestNotificationWhenUserRegisteredInteractor, RequestNotificationWhenUserRegisteredInteractor>()
            .AddScoped<ISendNotificationWhenNotificationRequestedInteractor, SendNotificationWhenNotificationRequestedInteractor>();
}