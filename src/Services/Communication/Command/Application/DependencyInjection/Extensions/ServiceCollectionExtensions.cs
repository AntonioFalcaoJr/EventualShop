using Application.Abstractions.Interactors;
using Application.UseCases;
using Microsoft.Extensions.DependencyInjection;
using Identity = Contracts.Services.Identity;

namespace Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandInteractors(this IServiceCollection services)
        => services;
    //  .AddScoped<IInteractor<Identity.Command.ChangePassword>, ChangePasswordInteractor>();

    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Identity.DomainEvent.UserRegistered>, RequestEmailConfirmationInteractor>();
}