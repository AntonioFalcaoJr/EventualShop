using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectDetailsWhenCatalogCreatedConsumer : Consumer<DomainEvent.CatalogCreated>
{
    public ProjectDetailsWhenCatalogCreatedConsumer(IProjectCatalogDetailsWhenCatalogCreatedInteractor interactor)
        : base(interactor) { }
}