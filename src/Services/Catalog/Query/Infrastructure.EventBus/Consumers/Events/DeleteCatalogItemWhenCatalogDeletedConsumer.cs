using Application.Abstractions;
using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class DeleteCatalogItemWhenCatalogDeletedConsumer : Consumer<DomainEvent.CatalogDeleted>
{
    public DeleteCatalogItemWhenCatalogDeletedConsumer(IDeleteCatalogItemWhenCatalogDeletedInteractor interactor)
        : base(interactor) { }
}