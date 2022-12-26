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
            .AddScoped<IInteractor<DomainEvent.CatalogCreated>, ProjectCatalogGridItemInteractor>()
            .AddScoped<IInteractor<DomainEvent.CatalogDeleted>, ProjectCatalogGridItemInteractor>()
            .AddScoped<IInteractor<DomainEvent.CatalogActivated>, ProjectCatalogGridItemInteractor>()
            .AddScoped<IInteractor<DomainEvent.CatalogDeactivated>, ProjectCatalogGridItemInteractor>()
            .AddScoped<IInteractor<DomainEvent.CatalogDescriptionChanged>, ProjectCatalogGridItemInteractor>()
            .AddScoped<IInteractor<DomainEvent.CatalogTitleChanged>, ProjectCatalogGridItemInteractor>()
            .AddScoped<IInteractor<DomainEvent.CatalogItemAdded>, ProjectCatalogItemCardInteractor>()
            .AddScoped<IInteractor<DomainEvent.CatalogItemAdded>, ProjectCatalogItemDetailsInteractor>()
            .AddScoped<IInteractor<DomainEvent.CatalogItemAdded>, ProjectCatalogItemListItemInteractor>()
            .AddScoped<IInteractor<DomainEvent.CatalogItemRemoved>, ProjectCatalogItemListItemInteractor>()
            .AddScoped<IInteractor<DomainEvent.CatalogDeleted>, ProjectCatalogItemListItemInteractor>();

    public static IServiceCollection AddQueryInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Query.GetCatalogItemDetails, Projection.CatalogItemDetails>, GetCatalogItemDetailsInteractor>()
            .AddScoped<IInteractor<Query.ListCatalogItemsCards, IPagedResult<Projection.CatalogItemCard>>, ListCatalogItemsCardsInteractor>()
            .AddScoped<IInteractor<Query.ListCatalogsGridItems, IPagedResult<Projection.CatalogGridItem>>, ListCatalogsGridItemsInteractor>()
            .AddScoped<IInteractor<Query.ListCatalogItemsListItems, IPagedResult<Projection.CatalogItemListItem>>, ListCatalogItemsListItemsInteractor>();
}