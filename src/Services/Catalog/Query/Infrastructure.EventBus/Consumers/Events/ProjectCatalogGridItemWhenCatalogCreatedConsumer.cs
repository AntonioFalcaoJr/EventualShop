using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectCatalogGridItemWhenCatalogCreatedConsumer : Consumer<DomainEvent.CatalogCreated>
{
    public ProjectCatalogGridItemWhenCatalogCreatedConsumer(IProjectCatalogGridItemWhenCatalogCreatedInteractor interactor)
        : base(interactor) { }
}