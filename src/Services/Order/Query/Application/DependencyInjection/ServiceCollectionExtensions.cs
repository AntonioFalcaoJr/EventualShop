using Application.Abstractions;
using Application.UseCases.Events;
using Application.UseCases.Queries;
using Contracts.Services.Order;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInteractors(this IServiceCollection services)
        => services
            .AddEventInteractors()
            .AddQueryInteractors();

    private static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services.AddScoped<IProjectOrderDetailsWhenOrderChangedInteractor, ProjectOrderDetailsWhenOrderChangedInteractor>();

    private static IServiceCollection AddQueryInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Query.GetOrderDetails, Projection.OrderDetails>, GetOrderDetailsInteractor>()
            .AddScoped<IPagedInteractor<Query.ListOrdersGridItems, Projection.OrderGridItem>, ListOrdersGridItemsInteractor>();
}