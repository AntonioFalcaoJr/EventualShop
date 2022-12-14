using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ActivateCatalogWhenActivatedConsumer : Consumer<DomainEvent.CatalogActivated>
{
    public ActivateCatalogWhenActivatedConsumer(IActivateCatalogWhenActivatedInteractor interactor)
        : base(interactor) { }
}