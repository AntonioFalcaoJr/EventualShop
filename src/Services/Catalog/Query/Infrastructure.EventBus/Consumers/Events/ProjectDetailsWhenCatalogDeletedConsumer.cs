using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectDetailsWhenCatalogDeletedConsumer : Consumer<DomainEvent.CatalogDeleted>
{
    public ProjectDetailsWhenCatalogDeletedConsumer(IProjectDetailsWhenCatalogDeletedInteractor interactor)
        : base(interactor) { }
}