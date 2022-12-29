using Application.Abstractions;
using Application.UseCases.Events;
using Application.UseCases.Queries;
using Contracts.Abstractions.Paging;
using Contracts.Services.Account;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<DomainEvent.AccountCreated>, AccountCreatedInteractor>()
            .AddScoped<IInteractor<DomainEvent.AccountDeleted>, AccountDeletedInteractor>()
            .AddScoped<IInteractor<DomainEvent.BillingAddressAdded>, BillingAddressAddedInteractor>()
            .AddScoped<IInteractor<DomainEvent.ShippingAddressAdded>, ShippingAddressAddedInteractor>()
            .AddScoped<IInteractor<IntegrationEvent.ProjectionRebuilt>, AccountRebuiltInteractor>()
            .AddScoped<IInteractor<IntegrationEvent.ProjectionRebuilt>, BillingAddressRebuiltInteractor>()
            .AddScoped<IInteractor<IntegrationEvent.ProjectionRebuilt>, ShippingAddressRebuiltInteractor>()
            .AddScoped<AccountRebuiltInteractor>()
            .AddScoped<BillingAddressRebuiltInteractor>()
            .AddScoped<ShippingAddressRebuiltInteractor>();

    public static IServiceCollection AddQueryInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Query.GetAccount, Projection.AccountDetails>, GetAccountInteractor>()
            .AddScoped<IInteractor<Query.ListAccounts, IPagedResult<Projection.AccountDetails>>, ListAccountsInteractor>()
            .AddScoped<IInteractor<Query.ListShippingAddresses, IPagedResult<Projection.ShippingAddressListItem>>, ListShippingAddressesInteractor>();
}