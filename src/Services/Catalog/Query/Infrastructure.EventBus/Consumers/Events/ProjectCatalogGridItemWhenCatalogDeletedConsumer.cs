using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectCatalogGridItemWhenCatalogDeletedConsumer : Consumer<DomainEvent.CatalogDeleted>
{
    public ProjectCatalogGridItemWhenCatalogDeletedConsumer(IProjectCatalogGridItemWhenCatalogDeletedInteractor interactor)
        : base(interactor) { }
}