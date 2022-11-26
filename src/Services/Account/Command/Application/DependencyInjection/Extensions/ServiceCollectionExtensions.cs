using Application.Abstractions;
using Application.UseCases.Commands;
using Contracts.Services.Account;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Command.AddBillingAddress>, AddBillingAddressInteractor>()
            .AddScoped<IInteractor<Command.AddShippingAddress>, AddShippingAddressInteractor>()
            .AddScoped<IInteractor<Command.DeleteAccount>, DeleteAccountInteractor>();
}