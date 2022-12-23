using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectCatalogGridItemWhenCatalogActivatedConsumer : Consumer<DomainEvent.CatalogActivated>
{
    public ProjectCatalogGridItemWhenCatalogActivatedConsumer(IProjectCatalogGridItemWhenCatalogActivatedInteractor interactor)
        : base(interactor) { }
}