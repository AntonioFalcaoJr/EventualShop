using Application.Abstractions;
using Application.UseCases.Events;
using Application.UseCases.Queries;
using Contracts.Abstractions.Paging;
using Contracts.Services.ShoppingCart;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services
            .AddScoped<IProjectCartDetailsWhenCartChangedInteractor, ProjectCartDetailsWhenCartChangedInteractor>()
            .AddScoped<IProjectCartItemListItemWhenCartChangedInteractor, ProjectCartItemListItemWhenCartChangedInteractor>()
            .AddScoped<IProjectPaymentMethodListItemWhenCartChangedInteractor, ProjectPaymentMethodListItemWhenCartChangedInteractor>();

    public static IServiceCollection AddQueryInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Query.GetShoppingCartDetails, Projection.ShoppingCartDetails>, GetShoppingCartDetailsInteractor>()
            .AddScoped<IInteractor<Query.GetCustomerShoppingCartDetails, Projection.ShoppingCartDetails>, GetCustomerShoppingCartDetailsInteractor>()
            .AddScoped<IInteractor<Query.GetPaymentMethodDetails, Projection.PaymentMethodDetails>, GetPaymentMethodDetailsInteractor>()
            .AddScoped<IInteractor<Query.GetShoppingCartItemDetails, Projection.ShoppingCartItemDetails>, GetShoppingCartItemDetailsInteractor>()
            .AddScoped<IInteractor<Query.ListPaymentMethodsListItems, IPagedResult<Projection.PaymentMethodListItem>>, ListPaymentMethodsListItemsInteractor>()
            .AddScoped<IInteractor<Query.ListShoppingCartItemsListItems, IPagedResult<Projection.ShoppingCartItemListItem>>, ListShoppingCartItemsListItemsInteractor>();
}