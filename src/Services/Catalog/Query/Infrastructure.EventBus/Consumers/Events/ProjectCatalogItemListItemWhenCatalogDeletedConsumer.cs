using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectCatalogItemListItemWhenCatalogDeletedConsumer : Consumer<DomainEvent.CatalogDeleted>
{
    public ProjectCatalogItemListItemWhenCatalogDeletedConsumer(IProjectCatalogItemListItemWhenCatalogDeletedInteractor interactor)
        : base(interactor) { }
}