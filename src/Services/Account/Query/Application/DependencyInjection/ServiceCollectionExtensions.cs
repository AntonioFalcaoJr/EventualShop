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
            .AddScoped<IProjectAccountDetailsWhenAccountChangedInteractor, ProjectAccountDetailsWhenAccountChangedInteractor>()
            .AddScoped<IProjectBillingAddressListItemWhenAccountChangedInteractor, ProjectBillingAddressListItemWhenAccountChangedInteractor>()
            .AddScoped<IProjectShippingAddressListItemWhenAccountChangedInteractor, ProjectShippingAddressListItemWhenAccountChangedInteractor>();

    public static IServiceCollection AddQueryInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Query.GetAccountDetails, Projection.AccountDetails>, GetAccountInteractor>()
            .AddScoped<IInteractor<Query.ListAccountsDetails, IPagedResult<Projection.AccountDetails>>, ListAccountsInteractor>()
            .AddScoped<IInteractor<Query.ListShippingAddressesListItems, IPagedResult<Projection.ShippingAddressListItem>>, ListShippingAddressesInteractor>();
}