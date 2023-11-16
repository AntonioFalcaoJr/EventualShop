using Application.Abstractions;
using Application.Services;
using Application.UseCases.Commands;
using Application.UseCases.Events;
using Contracts.Boundaries.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services
            .AddScoped<IApplicationService, ApplicationService>();

    public static IServiceCollection AddCommandInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Command.RegisterUser>, RegisterUserInteractor>()
            .AddScoped<IInteractor<Command.ChangeEmail>, ChangeEmailInteractor>()
            .AddScoped<IInteractor<Command.ConfirmEmail>, ConfirmEmailInteractor>()
            .AddScoped<IInteractor<Command.ChangePassword>, ChangePasswordInteractor>();

    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<DomainEvent.UserRegistered>, UserRegisteredInteractor>()
            .AddScoped<IInteractor<DomainEvent.EmailChanged>, EmailChangedInteractor>()
            .AddScoped<IInteractor<DomainEvent.EmailConfirmed>, EmailConfirmedInteractor>()
            .AddScoped<IInteractor<DelayedEvent.EmailConfirmationExpired>, EmailConfirmationExpiredInteractor>()
            .AddScoped<IInteractor<Contracts.Boundaries.Account.DomainEvent.AccountDeactivated>, AccountDeactivatedInteractor>()
            .AddScoped<IInteractor<Contracts.Boundaries.Account.DomainEvent.AccountDeleted>, AccountDeletedInteractor>();
}