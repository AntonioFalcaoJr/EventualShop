using Application.Abstractions.Gateways;
using Application.Abstractions.Interactors;
using Application.Services;
using Application.UseCases;
using Microsoft.Extensions.DependencyInjection;
using Identity = Contracts.Services.Identity;
using Communication = Contracts.Services.Communication;

namespace Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
        => services
            .AddScoped<IApplicationService, ApplicationService>()
            .AddScoped<INotificationService, NotificationService>();
    
    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Identity.DomainEvent.UserRegistered>, UserRegisteredInteractor>()
            .AddScoped<IInteractor<Communication.DomainEvent.NotificationRequested>, NotificationRequestedInteractor>();
}