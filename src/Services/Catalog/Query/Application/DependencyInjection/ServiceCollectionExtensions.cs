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
            .AddScoped<ICreateCatalogWhenCreatedInteractor, CreateCatalogWhenCreatedInteractor>()
            .AddScoped<IDeleteCatalogWhenDeletedInteractor, DeleteCatalogWhenDeletedInteractor>()
            .AddScoped<IActivateCatalogWhenActivatedInteractor, ActivateCatalogWhenActivatedInteractor>()
            .AddScoped<IDeactivateCatalogWhenDeactivatedInteractor, DeactivateCatalogWhenDeactivatedInteractor>()
            .AddScoped<IDescriptionChangeCatalogWhenChangedInteractor, DescriptionChangeCatalogWhenChangedInteractor>()
            .AddScoped<ITitleChangeCatalogWhenChangedInteractor, TitleChangeCatalogWhenChangedInteractor>()
            .AddScoped<IAddCatalogItemWhenAddedInteractor, AddCatalogItemWhenAddedInteractor>()
            .AddScoped<IRemoveCatalogItemWhenRemovedInteractor, RemoveCatalogItemWhenRemovedInteractor>()
            .AddScoped<IDeleteCatalogItemWhenCatalogDeletedInteractor, DeleteCatalogItemWhenCatalogDeletedInteractor>();

    public static IServiceCollection AddQueryInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Query.GetCatalog, Projection.Catalog>, GetCatalogInteractor>()
            .AddScoped<IInteractor<Query.GetCatalogItems, Projection.CatalogItem>, GetCatalogItemInteractor>()
            .AddScoped<IInteractor<Query.GetCatalogs, IPagedResult<Projection.Catalog>>, ListCatalogsInteractor>()
            .AddScoped<IInteractor<Query.GetAllItems, IPagedResult<Projection.CatalogItem>>, ListCatalogItemsInteractor>();
}