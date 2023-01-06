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
            .AddScoped<IProjectInventoryDetailsWhenChangedInteractor, ProjectInventoryDetailsWhenChangedInteractor>()
            .AddScoped<IProjectInventoryItemWhenChangedInteractor, ProjectInventoryItemWhenChangedInteractor>();

    public static IServiceCollection AddQueryInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Query.ListInventoryGridItems, IPagedResult<Projection.Inventory>>, ListInventoriesGridInteractor>()
            .AddScoped<IInteractor<Query.ListInventoryItems, IPagedResult<Projection.InventoryItem>>, ListInventoryItemsInteractor>();
}