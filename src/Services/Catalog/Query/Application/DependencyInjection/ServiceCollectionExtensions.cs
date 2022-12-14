using Application.UseCases.Events;
using Application.UseCases.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services
            .AddScoped<IProjectCatalogDetailsWhenCatalogCreatedInteractor, ProjectCatalogDetailsWhenCatalogCreatedInteractor>()
            .AddScoped<IProjectCatalogDetailsWhenCatalogDeletedInteractor, ProjectCatalogDetailsWhenCatalogDeletedInteractor>()
            .AddScoped<IProjectCatalogDetailsWhenCatalogActivatedInteractor, ProjectCatalogDetailsWhenCatalogActivatedInteractor>()
            .AddScoped<IProjectCatalogDetailsWhenCatalogDeactivatedInteractor, ProjectCatalogDetailsWhenCatalogDeactivatedInteractor>()
            .AddScoped<IProjectCatalogDetailsWhenCatalogDescriptionChangedInteractor, ProjectCatalogDetailsWhenCatalogDescriptionChangedInteractor>()
            .AddScoped<IProjectCatalogDetailsWhenCatalogTitleChangedInteractor, ProjectCatalogDetailsWhenCatalogTitleChangedInteractor>()
            .AddScoped<IProjectCatalogItemListItemWhenCatalogItemAddedInteractor, ProjectCatalogItemListItemWhenCatalogItemAddedInteractor>()
            .AddScoped<IProjectCatalogItemListItemWhenCatalogItemRemovedInteractor, ProjectCatalogItemListItemWhenCatalogItemRemovedInteractor>()
            .AddScoped<IProjectCatalogItemListItemWhenCatalogDeletedInteractor, ProjectCatalogItemListItemWhenCatalogDeletedInteractor>();

    public static IServiceCollection AddQueryInteractors(this IServiceCollection services)
        => services
            .AddScoped<IGetCatalogItemDetailsInteractor, GetCatalogItemDetailsInteractor>()
            .AddScoped<IListCatalogItemsCardsInteractor, ListCatalogItemsCardsInteractor>()
            .AddScoped<IListCatalogsGridItemsInteractor, ListCatalogsGridItemsInteractor>()
            .AddScoped<IListCatalogItemsListItemsInteractor, ListCatalogItemsListItemsInteractor>();
}