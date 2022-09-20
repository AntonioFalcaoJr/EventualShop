using Application.Abstractions.Interactors;
using Application.UseCases.Commands;
using Application.UseCases.Events;
using Microsoft.Extensions.DependencyInjection;
using Account = Contracts.Services.Account;
using Identity = Contracts.Services.Identity;

namespace Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Identity.Command.RegisterUser>, RegisterUserInteractor>()
            .AddScoped<IInteractor<Identity.Command.ChangeEmail>, ChangeEmailInteractor>()
            .AddScoped<IInteractor<Identity.Command.VerifyEmail>, VerifyEmailInteractor>()
            .AddScoped<IInteractor<Identity.Command.ChangePassword>, ChangePasswordInteractor>();

    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Identity.DomainEvent.UserRegistered>, ScheduleEmailConfirmationInteractor>()
            .AddScoped<IInteractor<Identity.DomainEvent.EmailChanged>, ScheduleEmailConfirmationInteractor>()
            .AddScoped<IInteractor<Identity.DomainEvent.EmailVerified>, DefinePrimaryEmailInteractor>()
            .AddScoped<IInteractor<Identity.DelayedEvent.EmailConfirmationExpired>, ExpireEmailInteractor>()
            .AddScoped<IInteractor<Account.DomainEvent.AccountDeactivated>, DeactivateUserInteractor>()
            .AddScoped<IInteractor<Account.DomainEvent.AccountDeleted>, DeleteUserInteractor>();
}