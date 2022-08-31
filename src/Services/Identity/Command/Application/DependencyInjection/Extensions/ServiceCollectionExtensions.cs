using Application.Abstractions.Interactors;
using Application.UseCases;
using Contracts.Services.Identity;
using Microsoft.Extensions.DependencyInjection;
using DomainEvent = Contracts.Services.Account.DomainEvent;

namespace Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Command.RegisterUser>, RegisterUserInteractor>()
            .AddScoped<IInteractor<Command.ChangePassword>, ChangePasswordInteractor>();

    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<DomainEvent.AccountDeactivated>, DeactivateUserInteractor>()
            .AddScoped<IInteractor<DomainEvent.AccountDeleted>, DeleteUserInteractor>();
}