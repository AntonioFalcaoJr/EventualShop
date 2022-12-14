using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class DeactivateCatalogWhenDeactivatedConsumer : Consumer<DomainEvent.CatalogDeactivated>
{
    public DeactivateCatalogWhenDeactivatedConsumer(IDeactivateCatalogWhenDeactivatedInteractor interactor)
        : base(interactor) { }
}