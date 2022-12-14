using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectDetailsWhenCatalogActivatedConsumer : Consumer<DomainEvent.CatalogActivated>
{
    public ProjectDetailsWhenCatalogActivatedConsumer(IProjectCatalogDetailsWhenCatalogActivatedInteractor interactor)
        : base(interactor) { }
}