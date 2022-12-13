using Application.Abstractions;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers;

public class CatalogTitleChangedConsumer : Consumer<DomainEvent.CatalogTitleChanged>
{
    public CatalogTitleChangedConsumer(IInteractor<DomainEvent.CatalogTitleChanged> interactor)
        : base(interactor) { }
}