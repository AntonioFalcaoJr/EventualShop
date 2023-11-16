using Application.Abstractions;
using Application.UseCases.Events;
using Application.UseCases.Queries;
using Contracts.Boundaries.Account;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInteractors(this IServiceCollection services)
        => services
            .AddEventInteractors()
            .AddQueryInteractors();

    private static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services
            .AddScoped<IProjectAccountDetailsWhenAccountChangedInteractor, ProjectAccountDetailsWhenAccountChangedInteractor>()
            .AddScoped<IProjectBillingAddressListItemWhenAccountChangedInteractor, ProjectBillingAddressListItemWhenAccountChangedInteractor>()
            .AddScoped<IProjectShippingAddressListItemWhenAccountChangedInteractor, ProjectShippingAddressListItemWhenAccountChangedInteractor>();

    private static IServiceCollection AddQueryInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Query.GetAccountDetails, Projection.AccountDetails>, GetAccountInteractor>()
            .AddScoped<IPagedInteractor<Query.ListAccountsDetails, Projection.AccountDetails>, ListAccountsInteractor>()
            .AddScoped<IPagedInteractor<Query.ListShippingAddressesListItems, Projection.ShippingAddressListItem>, ListShippingAddressesInteractor>();
}