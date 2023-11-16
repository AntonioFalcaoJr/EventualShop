using Application.Abstractions;
using Application.UseCases.Events;
using Application.UseCases.Queries;
using Contracts.Boundaries.Cataloging.Catalog;
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
            .AddScoped<IProjectCatalogGridItemWhenCatalogChangedInteractor, ProjectCatalogGridItemWhenCatalogChangedInteractor>()
            .AddScoped<IProjectCatalogItemCardWhenCatalogChangedInteractor, ProjectCatalogItemCardWhenCatalogChangedInteractor>()
            .AddScoped<IProjectCatalogItemDetailsWhenCatalogChangedInteractor, ProjectCatalogItemDetailsWhenCatalogChangedInteractor>()
            .AddScoped<IProjectCatalogItemListItemWhenCatalogChangedInteractor, ProjectCatalogItemListItemWhenCatalogChangedInteractor>();

    private static IServiceCollection AddQueryInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Query.GetCatalogItemDetails, Projection.CatalogItemDetails>, GetCatalogItemDetailsInteractor>()
            .AddScoped<IPagedInteractor<Query.ListCatalogItemsCards, Projection.CatalogItemCard>, ListCatalogItemsCardsInteractor>()
            .AddScoped<IPagedInteractor<Query.ListCatalogsGridItems, Projection.CatalogGridItem>, ListCatalogsGridItemsInteractor>()
            .AddScoped<IPagedInteractor<Query.ListCatalogItemsListItems, Projection.CatalogItemListItem>, ListCatalogItemsListItemsInteractor>();
}