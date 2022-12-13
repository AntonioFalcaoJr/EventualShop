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
            .AddScoped<IInteractor<DomainEvent.CatalogCreated>, CatalogCreatedInteractor>()
            .AddScoped<IInteractor<DomainEvent.CatalogDeleted>, CatalogDeletedInteractor>()
            .AddScoped<IInteractor<DomainEvent.CatalogActivated>, CatalogActivatedInteractor>()
            .AddScoped<IInteractor<DomainEvent.CatalogDeactivated>, CatalogDeactivatedInteractor>()
            .AddScoped<IInteractor<DomainEvent.CatalogDescriptionChanged>, CatalogDescriptionChangedInteractor>()
            .AddScoped<IInteractor<DomainEvent.CatalogTitleChanged>, CatalogTitleChangedInteractor>()
            .AddScoped<IInteractor<DomainEvent.CatalogItemAdded>, CatalogItemAddedInteractor>()
            .AddScoped<IInteractor<DomainEvent.CatalogItemRemoved>, CatalogItemRemovedInteractor>();

    public static IServiceCollection AddQueryInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Query.GetCatalog, Projection.Catalog>, GetCatalogInteractor>()
            .AddScoped<IInteractor<Query.GetCatalogItems, Projection.CatalogItem>, GetCatalogItemInteractor>()
            .AddScoped<IInteractor<Query.GetCatalogs, IPagedResult<Projection.Catalog>>, ListCatalogsInteractor>()
            .AddScoped<IInteractor<Query.GetAllItems, IPagedResult<Projection.CatalogItem>>, ListCatalogItemsInteractor>();
}