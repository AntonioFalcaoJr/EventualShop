using Application.Abstractions;
using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class DeleteCatalogWhenDeletedConsumer : Consumer<DomainEvent.CatalogDeleted>
{
    public DeleteCatalogWhenDeletedConsumer(IDeleteCatalogWhenDeletedInteractor interactor)
        : base(interactor) { }
}