using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectCatalogGridItemWhenCatalogDeactivatedConsumer : Consumer<DomainEvent.CatalogDeactivated>
{
    public ProjectCatalogGridItemWhenCatalogDeactivatedConsumer(IProjectCatalogGridItemWhenCatalogDeactivatedInteractor interactor)
        : base(interactor) { }
}