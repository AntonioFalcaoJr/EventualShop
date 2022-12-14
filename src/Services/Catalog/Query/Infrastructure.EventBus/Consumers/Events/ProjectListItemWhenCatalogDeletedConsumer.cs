using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectListItemWhenCatalogDeletedConsumer : Consumer<DomainEvent.CatalogDeleted>
{
    public ProjectListItemWhenCatalogDeletedConsumer(IProjectCatalogItemListItemWhenCatalogDeletedInteractor interactor)
        : base(interactor) { }
}