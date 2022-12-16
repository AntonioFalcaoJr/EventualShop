using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectCatalogItemCardWhenCatalogItemAddedConsumer : Consumer<DomainEvent.CatalogItemAdded>
{
    public ProjectCatalogItemCardWhenCatalogItemAddedConsumer(IProjectCatalogItemCardWhenCatalogItemAddedInteractor interactor)
        : base(interactor) { }
}