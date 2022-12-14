using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectListItemWhenCatalogItemRemovedConsumer : Consumer<DomainEvent.CatalogItemRemoved>
{
    public ProjectListItemWhenCatalogItemRemovedConsumer(IProjectListItemWhenCatalogItemRemovedInteractor interactor)
        : base(interactor) { }
}