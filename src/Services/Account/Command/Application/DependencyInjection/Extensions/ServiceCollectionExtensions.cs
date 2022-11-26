using Application.Abstractions;
using Application.Services;
using Application.UseCases.Commands;
using Application.UseCases.Events;
using Contracts.Services.Account;
using Microsoft.Extensions.DependencyInjection;
using Identity = Contracts.Services.Identity;

namespace Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services
            .AddScoped<IApplicationService, ApplicationService>();
    
    public static IServiceCollection AddCommandInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Command.AddBillingAddress>, AddBillingAddressInteractor>()
            .AddScoped<IInteractor<Command.AddShippingAddress>, AddShippingAddressInteractor>()
            .AddScoped<IInteractor<Command.DeleteAccount>, DeleteAccountInteractor>();

    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Identity.DomainEvent.UserRegistered>, UserRegisteredInteractor>();
}