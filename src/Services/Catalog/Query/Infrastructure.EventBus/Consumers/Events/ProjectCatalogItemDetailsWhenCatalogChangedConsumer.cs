using Application.UseCases.Events;
using Contracts.Services.Catalog;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectCatalogItemDetailsWhenCatalogChangedConsumer : IConsumer<DomainEvent.CatalogItemAdded>
{
    private readonly IProjectCatalogItemDetailsWhenCatalogChangedInteractor _interactor;

    public ProjectCatalogItemDetailsWhenCatalogChangedConsumer(IProjectCatalogItemDetailsWhenCatalogChangedInteractor interactor)
    {
        _interactor = interactor;
    }

    public Task Consume(ConsumeContext<DomainEvent.CatalogItemAdded> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);
}