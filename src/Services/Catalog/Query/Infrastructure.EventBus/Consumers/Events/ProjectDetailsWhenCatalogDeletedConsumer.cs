using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectDetailsWhenCatalogDeletedConsumer : Consumer<DomainEvent.CatalogDeleted>
{
    public ProjectDetailsWhenCatalogDeletedConsumer(IProjectCatalogDetailsWhenCatalogDeletedInteractor interactor)
        : base(interactor) { }
}