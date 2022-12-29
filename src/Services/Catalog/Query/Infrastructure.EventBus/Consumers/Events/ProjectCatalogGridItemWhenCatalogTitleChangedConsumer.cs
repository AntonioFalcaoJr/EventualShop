using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectCatalogGridItemWhenCatalogTitleChangedConsumer : Consumer<DomainEvent.CatalogTitleChanged>
{
    public ProjectCatalogGridItemWhenCatalogTitleChangedConsumer(IProjectCatalogGridItemWhenCatalogTitleChangedInteractor interactor)
        : base(interactor) { }
}