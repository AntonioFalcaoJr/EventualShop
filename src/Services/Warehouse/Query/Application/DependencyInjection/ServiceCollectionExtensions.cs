using Application.Abstractions;
using Application.UseCases.Events;
using Application.UseCases.Queries;
using Contracts.Abstractions.Paging;
using Contracts.Services.Warehouse;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services
            .AddScoped<IProjectInventoryGridItemWhenInventoryChangedInteractor, ProjectInventoryGridItemWhenInventoryChangedInteractor>()
            .AddScoped<IProjectInventoryItemListItemWhenInventoryItemChangedInteractor, ProjectInventoryItemListItemWhenInventoryItemChangedInteractor>();

    public static IServiceCollection AddQueryInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Query.ListInventoryGridItems, IPagedResult<Projection.InventoryGridItem>>, ListInventoriesGridItemInteractor>()
            .AddScoped<IInteractor<Query.ListInventoryItemsListItems, IPagedResult<Projection.InventoryItemListItem>>, ListInventoryItemsListItemsInteractor>();
}