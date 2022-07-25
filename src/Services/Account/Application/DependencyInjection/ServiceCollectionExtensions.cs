using Application.Abstractions.UseCases;
using Application.UseCases.Commands.AddBillingAddress;
using Application.UseCases.Commands.AddShippingAddress;
using Application.UseCases.Commands.CreateAccount;
using Application.UseCases.Commands.DeleteAccount;
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
            .AddScoped<IInteractor<Command.DeleteAccount>, DeleteAccountInteractor>()
            .AddScoped<IInteractor<DomainEvent.AccountCreated>, ProjectAccountCreatedInteractor>()
            .AddScoped<IInteractor<DomainEvent.AccountDeleted>, ProjectAccountDeletedInteractor>()
            .AddScoped<IInteractor<DomainEvent.BillingAddressAdded>, ProjectBillingAddressAddedInteractor>()
            .AddScoped<IInteractor<DomainEvent.ShippingAddressAdded>, ProjectShippingAddressAddedInteractor>();
}