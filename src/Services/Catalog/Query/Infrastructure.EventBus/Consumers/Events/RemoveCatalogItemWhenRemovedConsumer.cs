using Application.Abstractions;
using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class RemoveCatalogItemWhenRemovedConsumer : Consumer<DomainEvent.CatalogItemRemoved>
{
    public RemoveCatalogItemWhenRemovedConsumer(IRemoveCatalogItemWhenRemovedInteractor interactor)
        : base(interactor) { }
}