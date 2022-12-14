using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectListItemWhenCatalogItemAddedConsumer : Consumer<DomainEvent.CatalogItemAdded>
{
    public ProjectListItemWhenCatalogItemAddedConsumer(IProjectCatalogItemListItemWhenCatalogItemAddedInteractor interactor)
        : base(interactor) { }
}