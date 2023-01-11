using Application.Abstractions;
using Application.Services;
using Application.UseCases.Commands;
using Application.UseCases.Events;
using Microsoft.Extensions.DependencyInjection;
using Account = Contracts.Services.Account;
using Identity = Contracts.Services.Identity;

namespace Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services
            .AddScoped<IApplicationService, ApplicationService>();
    
    public static IServiceCollection AddCommandInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Identity.Command.SignUp>, SignUpInteractor>()
            .AddScoped<IInteractor<Identity.Command.ChangeEmail>, ChangeEmailInteractor>()
            .AddScoped<IInteractor<Identity.Command.ConfirmEmail>, ConfirmEmailInteractor>()
            .AddScoped<IInteractor<Identity.Command.ChangePassword>, ChangePasswordInteractor>();

    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Identity.DomainEvent.UserRegistered>, UserRegisteredInteractor>()
            .AddScoped<IInteractor<Identity.DomainEvent.EmailChanged>, EmailChangedInteractor>()
            .AddScoped<IInteractor<Identity.DomainEvent.EmailConfirmed>, EmailConfirmedInteractor>()
            .AddScoped<IInteractor<Identity.DelayedEvent.EmailConfirmationExpired>, EmailConfirmationExpiredInteractor>()
            .AddScoped<IInteractor<Account.DomainEvent.AccountDeactivated>, AccountDeactivatedInteractor>()
            .AddScoped<IInteractor<Account.DomainEvent.AccountDeleted>, AccountDeletedInteractor>();
}