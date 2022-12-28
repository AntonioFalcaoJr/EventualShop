using Application.Abstractions;
using Application.UseCases.Events;
using Application.UseCases.Queries;
using Contracts.Abstractions.Paging;
using Contracts.Services.Catalog;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services
            .AddScoped<IProjectCatalogGridItemInteractor, ProjectCatalogGridItemInteractor>()
            .AddScoped<IProjectCatalogItemCardInteractor, ProjectCatalogItemCardInteractor>()
            .AddScoped<IProjectCatalogItemDetailsInteractor, ProjectCatalogItemDetailsInteractor>()
            .AddScoped<IProjectCatalogItemListItemInteractor, ProjectCatalogItemListItemInteractor>();

    public static IServiceCollection AddQueryInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Query.GetCatalogItemDetails, Projection.CatalogItemDetails>, GetCatalogItemDetailsInteractor>()
            .AddScoped<IInteractor<Query.ListCatalogItemsCards, IPagedResult<Projection.CatalogItemCard>>, ListCatalogItemsCardsInteractor>()
            .AddScoped<IInteractor<Query.ListCatalogsGridItems, IPagedResult<Projection.CatalogGridItem>>, ListCatalogsGridItemsInteractor>()
            .AddScoped<IInteractor<Query.ListCatalogItemsListItems, IPagedResult<Projection.CatalogItemListItem>>, ListCatalogItemsListItemsInteractor>();
}