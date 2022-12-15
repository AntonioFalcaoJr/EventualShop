using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectCatalogGridItemWhenCatalogDescriptionChangedConsumer : Consumer<DomainEvent.CatalogDescriptionChanged>
{
    public ProjectCatalogGridItemWhenCatalogDescriptionChangedConsumer(IProjectCatalogGridItemWhenCatalogDescriptionChangedInteractor interactor)
        : base(interactor) { }
}