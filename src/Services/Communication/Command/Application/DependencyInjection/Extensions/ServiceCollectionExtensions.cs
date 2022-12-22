using Application.Abstractions.Gateways;
using Application.Abstractions.Interactors;
using Application.Services;
using Application.UseCases;
using Application.UseCases.Events;
using Microsoft.Extensions.DependencyInjection;
using Identity = Contracts.Services.Identity;
using Communication = Contracts.Services.Communication;

namespace Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services
            .AddScoped<IApplicationService, ApplicationService>();

    public static IServiceCollection AddCommandInteractors(this IServiceCollection services)
        => services;

    public static IServiceCollection AddNotificationGateway(this IServiceCollection services)
        => services
            .AddScoped<INotificationGateway, NotificationGateway>()
            .AddScoped<NotificationOptionGatewayProvider>();

    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Identity.DomainEvent.UserRegistered>, UserRegisteredInteractor>()
            .AddScoped<IInteractor<Communication.DomainEvent.NotificationRequested>, NotificationRequestedInteractor>();
}