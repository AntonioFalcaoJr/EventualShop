using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class CreateCatalogWhenCreatedConsumer : Consumer<DomainEvent.CatalogCreated>
{
    public CreateCatalogWhenCreatedConsumer(ICreateCatalogWhenCreatedInteractor interactor)
        : base(interactor) { }
}