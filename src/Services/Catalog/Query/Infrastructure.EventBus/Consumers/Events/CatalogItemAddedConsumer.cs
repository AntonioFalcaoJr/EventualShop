using Application.Abstractions;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class CatalogItemAddedConsumer : Consumer<DomainEvent.CatalogItemAdded>
{
    public CatalogItemAddedConsumer(IInteractor<DomainEvent.CatalogItemAdded> interactor)
        : base(interactor) { }
}