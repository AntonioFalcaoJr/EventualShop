using Application.Abstractions;
using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class DescriptionChangeCatalogWhenChangedConsumer : Consumer<DomainEvent.CatalogDescriptionChanged>
{
    public DescriptionChangeCatalogWhenChangedConsumer(IDescriptionChangeCatalogWhenChangedInteractor interactor)
        : base(interactor) { }
}