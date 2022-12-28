using Application.Abstractions;
using Application.UseCases.Events;
using Application.UseCases.Queries;
using Contracts.Abstractions.Paging;
using Contracts.Services.Order;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<DomainEvent.OrderPlaced>, OrderPlacedInteractor>()
            .AddScoped<IInteractor<DomainEvent.OrderConfirmed>, OrderConfirmedInteractor>();

    public static IServiceCollection AddQueryInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Query.GetOrderDetails, Projection.OrderDetails>, GetOrderDetailsInteractor>()
            .AddScoped<IInteractor<Query.ListOrdersGridItems, IPagedResult<Projection.OrderGridItem>>, ListOrdersGridItemsInteractor>();
}