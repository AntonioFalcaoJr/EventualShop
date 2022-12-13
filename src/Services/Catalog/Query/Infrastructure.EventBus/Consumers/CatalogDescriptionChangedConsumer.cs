using Application.Abstractions;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers;

public class CatalogDescriptionChangedConsumer : Consumer<DomainEvent.CatalogDescriptionChanged>
{
    public CatalogDescriptionChangedConsumer(IInteractor<DomainEvent.CatalogDescriptionChanged> interactor)
        : base(interactor) { }
}