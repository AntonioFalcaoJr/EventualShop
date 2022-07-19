using Application.Abstractions.UseCases;
using Application.UseCases.Commands;
using Contracts.Services.Account;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Command.AddBillingAddress>, AddBillingAddressInteractor>()
            .AddScoped<IInteractor<Command.AddShippingAddress>, AddShippingAddressInteractor>()
            .AddScoped<IInteractor<Command.CreateAccount>, CreateAccountInteractor>()
            .AddScoped<IInteractor<Command.DeleteAccount>, DeleteAccountInteractor>();
}