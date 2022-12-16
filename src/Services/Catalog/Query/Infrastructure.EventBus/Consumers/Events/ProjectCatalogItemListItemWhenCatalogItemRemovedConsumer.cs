using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectCatalogItemListItemWhenCatalogItemRemovedConsumer : Consumer<DomainEvent.CatalogItemRemoved>
{
    public ProjectCatalogItemListItemWhenCatalogItemRemovedConsumer(IProjectCatalogItemListItemWhenCatalogItemRemovedInteractor interactor)
        : base(interactor) { }
}