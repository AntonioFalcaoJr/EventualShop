using Application.Abstractions;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers;

public class CatalogCreatedConsumer : Consumer<DomainEvent.CatalogCreated>
{
    public CatalogCreatedConsumer(IInteractor<DomainEvent.CatalogCreated> interactor)
        : base(interactor) { }
}