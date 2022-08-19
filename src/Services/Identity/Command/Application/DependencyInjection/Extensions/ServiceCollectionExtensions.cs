using Application.Abstractions.Interactors;
using Application.UseCases;
using Contracts.Services.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Command.RegisterUser>, RegisterUserInteractor>()
            .AddScoped<IInteractor<Command.ChangePassword>, ChangePasswordInteractor>()
            .AddScoped<IInteractor<Command.DeleteUser>, DeleteUserInteractor>();
}