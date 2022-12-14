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
            .AddScoped<IProjectDetailsWhenCatalogCreatedInteractor, ProjectDetailsWhenCatalogCreatedInteractor>()
            .AddScoped<IProjectDetailsWhenCatalogDeletedInteractor, ProjectDetailsWhenCatalogDeletedInteractor>()
            .AddScoped<IProjectDetailsWhenCatalogActivatedInteractor, ProjectDetailsWhenCatalogActivatedInteractor>()
            .AddScoped<IProjectDetailsWhenCatalogDeactivatedInteractor, ProjectDetailsWhenCatalogDeactivatedInteractor>()
            .AddScoped<IProjectDetailsWhenCatalogDescriptionChangedInteractor, ProjectDetailsWhenCatalogDescriptionChangedInteractor>()
            .AddScoped<IProjectDetailsWhenCatalogTitleChangedInteractor, ProjectDetailsWhenCatalogTitleChangedInteractor>()
            .AddScoped<IProjectListItemWhenCatalogItemAddedInteractor, ProjectListItemWhenCatalogItemAddedInteractor>()
            .AddScoped<IProjectListItemWhenCatalogItemRemovedInteractor, ProjectListItemWhenCatalogItemRemovedInteractor>()
            .AddScoped<IProjectListItemWhenCatalogDeletedInteractor, ProjectListItemWhenCatalogDeletedInteractor>();

    public static IServiceCollection AddQueryInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Query.GetCatalog, Projection.CatalogDetails>, GetCatalogInteractor>()
            .AddScoped<IInteractor<Query.GetCatalogItems, Projection.CatalogItemListItem>, GetCatalogItemInteractor>()
            .AddScoped<IInteractor<Query.GetCatalogs, IPagedResult<Projection.CatalogDetails>>, ListCatalogsInteractor>()
            .AddScoped<IInteractor<Query.GetAllItems, IPagedResult<Projection.CatalogItemListItem>>, ListCatalogItemsInteractor>();
}