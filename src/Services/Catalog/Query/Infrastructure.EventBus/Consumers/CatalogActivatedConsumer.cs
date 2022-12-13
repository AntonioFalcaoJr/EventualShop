using Application.Abstractions;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers;

public class CatalogActivatedConsumer : Consumer<DomainEvent.CatalogActivated>
{
    public CatalogActivatedConsumer(IInteractor<DomainEvent.CatalogActivated> interactor)
        : base(interactor) { }
}