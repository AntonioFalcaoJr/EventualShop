using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectCatalogItemListItemWhenCatalogItemAddedConsumer : Consumer<DomainEvent.CatalogItemAdded>
{
    public ProjectCatalogItemListItemWhenCatalogItemAddedConsumer(IProjectCatalogItemListItemWhenCatalogItemAddedInteractor interactor)
        : base(interactor) { }
}