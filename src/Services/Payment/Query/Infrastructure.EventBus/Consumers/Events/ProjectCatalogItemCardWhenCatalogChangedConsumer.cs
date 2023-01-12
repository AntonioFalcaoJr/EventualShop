using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectCatalogItemCardWhenCatalogChangedConsumer : Consumer<DomainEvent.CatalogItemAdded>
{
    public ProjectCatalogItemCardWhenCatalogChangedConsumer(IProjectCatalogItemCardWhenCatalogChangedInteractor interactor)
        : base(interactor) { }
}