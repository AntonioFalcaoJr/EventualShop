using Application.Abstractions.Interactors;
using Application.UseCases.Commands;
using Application.UseCases.Events;
using Contracts.Services.Identity;
using Microsoft.Extensions.DependencyInjection;
using AccountDomainEvent = Contracts.Services.Account.DomainEvent;

namespace Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Command.RegisterUser>, RegisterUserInteractor>()
            .AddScoped<IInteractor<Command.ChangeEmail>, ChangeEmailInteractor>()
            .AddScoped<IInteractor<Command.ChangePassword>, ChangePasswordInteractor>();

    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<DomainEvent.UserRegistered>, ScheduleEmailConfirmationInteractor>()
            .AddScoped<IInteractor<DomainEvent.EmailChanged>, ScheduleEmailConfirmationInteractor>()
            .AddScoped<IInteractor<DomainEvent.EmailVerified>, DefinePrimaryEmailWhenVerifiedInteractor>()
            .AddScoped<IInteractor<AccountDomainEvent.AccountDeactivated>, DeactivateUserWhenAccountDeactivatedInteractor>()
            .AddScoped<IInteractor<AccountDomainEvent.AccountDeleted>, DeleteUserWhenAccountDeletedInteractor>();
}