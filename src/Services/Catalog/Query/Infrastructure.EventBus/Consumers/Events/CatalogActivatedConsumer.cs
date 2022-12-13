using Application.Abstractions;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class CatalogActivatedConsumer : Consumer<DomainEvent.CatalogActivated>
{
    public CatalogActivatedConsumer(IInteractor<DomainEvent.CatalogActivated> interactor)
        : base(interactor) { }
}