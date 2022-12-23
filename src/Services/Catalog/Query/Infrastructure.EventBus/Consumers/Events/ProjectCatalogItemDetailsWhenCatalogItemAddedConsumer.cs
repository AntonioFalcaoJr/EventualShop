using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectCatalogItemDetailsWhenCatalogItemAddedConsumer : Consumer<DomainEvent.CatalogItemAdded>
{
    public ProjectCatalogItemDetailsWhenCatalogItemAddedConsumer(IProjectCatalogItemDetailsWhenCatalogItemAddedInteractor interactor)
        : base(interactor) { }
}