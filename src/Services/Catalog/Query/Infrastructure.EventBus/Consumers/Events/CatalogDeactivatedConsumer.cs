using Application.Abstractions;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class CatalogDeactivatedConsumer : Consumer<DomainEvent.CatalogDeactivated>
{
    public CatalogDeactivatedConsumer(IInteractor<DomainEvent.CatalogDeactivated> interactor)
        : base(interactor) { }
}