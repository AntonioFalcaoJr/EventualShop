using Application.UseCases.Events;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectDetailsWhenCatalogTitleChangedConsumer : Consumer<DomainEvent.CatalogTitleChanged>
{
    public ProjectDetailsWhenCatalogTitleChangedConsumer(IProjectDetailsWhenCatalogTitleChangedInteractor interactor)
        : base(interactor) { }
}