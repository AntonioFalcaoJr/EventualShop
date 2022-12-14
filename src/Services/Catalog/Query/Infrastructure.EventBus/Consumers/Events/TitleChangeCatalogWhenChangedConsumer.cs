using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class TitleChangeCatalogWhenChangedConsumer : Consumer<DomainEvent.CatalogTitleChanged>
{
    public TitleChangeCatalogWhenChangedConsumer(ITitleChangeCatalogWhenChangedInteractor interactor)
        : base(interactor) { }
}