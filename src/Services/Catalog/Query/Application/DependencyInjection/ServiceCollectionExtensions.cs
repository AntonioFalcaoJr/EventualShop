using Application.UseCases.Events;
using Application.UseCases.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services
            .AddScoped<IProjectCatalogGridItemWhenCatalogCreatedInteractor, ProjectCatalogGridItemWhenCatalogCreatedInteractor>()
            .AddScoped<IProjectCatalogGridItemWhenCatalogDeletedInteractor, ProjectCatalogGridItemWhenCatalogDeletedInteractor>()
            .AddScoped<IProjectCatalogGridItemWhenCatalogActivatedInteractor, ProjectCatalogGridItemWhenCatalogActivatedInteractor>()
            .AddScoped<IProjectCatalogGridItemWhenCatalogDeactivatedInteractor, ProjectCatalogGridItemWhenCatalogDeactivatedInteractor>()
            .AddScoped<IProjectCatalogGridItemWhenCatalogDescriptionChangedInteractor, ProjectCatalogGridItemWhenCatalogDescriptionChangedInteractor>()
            .AddScoped<IProjectCatalogGridItemWhenCatalogTitleChangedInteractor, ProjectCatalogGridItemWhenCatalogTitleChangedInteractor>()
            .AddScoped<IProjectCatalogItemCardWhenCatalogItemAddedInteractor, ProjectCatalogItemCardWhenCatalogItemAddedInteractor>()
            .AddScoped<IProjectCatalogItemDetailsWhenCatalogItemAddedInteractor, ProjectCatalogItemDetailsWhenCatalogItemAddedInteractor>()
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