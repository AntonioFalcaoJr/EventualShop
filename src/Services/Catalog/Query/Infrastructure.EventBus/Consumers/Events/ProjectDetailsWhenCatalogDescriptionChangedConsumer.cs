using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectDetailsWhenCatalogDescriptionChangedConsumer : Consumer<DomainEvent.CatalogDescriptionChanged>
{
    public ProjectDetailsWhenCatalogDescriptionChangedConsumer(IProjectCatalogDetailsWhenCatalogDescriptionChangedInteractor interactor)
        : base(interactor) { }
}