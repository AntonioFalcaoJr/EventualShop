using Application.Abstractions;
using Application.UseCases.Events;
using Application.UseCases.Queries;
using Contracts.Boundaries.Shopping.ShoppingCart;
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
            .AddScoped<IProjectCartDetailsWhenCartChangedInteractor, ProjectCartDetailsWhenCartChangedInteractor>()
            .AddScoped<IProjectCartItemListItemWhenCartChangedInteractor, ProjectCartItemListItemWhenCartChangedInteractor>()
            .AddScoped<IProjectPaymentMethodListItemWhenCartChangedInteractor, ProjectPaymentMethodListItemWhenCartChangedInteractor>();

    private static IServiceCollection AddQueryInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Query.GetShoppingCartDetails, Projection.ShoppingCartDetails>, GetShoppingCartDetailsInteractor>()
            .AddScoped<IInteractor<Query.GetCustomerShoppingCartDetails, Projection.ShoppingCartDetails>, GetCustomerShoppingCartDetailsInteractor>()
            .AddScoped<IInteractor<Query.GetPaymentMethodDetails, Projection.PaymentMethodDetails>, GetPaymentMethodDetailsInteractor>()
            .AddScoped<IInteractor<Query.GetShoppingCartItemDetails, Projection.ShoppingCartItemDetails>, GetShoppingCartItemDetailsInteractor>()
            .AddScoped<IPagedInteractor<Query.ListPaymentMethodsListItems, Projection.PaymentMethodListItem>, ListPaymentMethodsListItemsInteractor>()
            .AddScoped<IPagedInteractor<Query.ListShoppingCartItemsListItems, Projection.ShoppingCartItemListItem>, ListShoppingCartItemsListItemsInteractor>();
}