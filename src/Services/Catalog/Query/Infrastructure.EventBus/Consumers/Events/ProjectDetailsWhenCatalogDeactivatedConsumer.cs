using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectDetailsWhenCatalogDeactivatedConsumer : Consumer<DomainEvent.CatalogDeactivated>
{
    public ProjectDetailsWhenCatalogDeactivatedConsumer(IProjectDetailsWhenCatalogDeactivatedInteractor interactor)
        : base(interactor) { }
}