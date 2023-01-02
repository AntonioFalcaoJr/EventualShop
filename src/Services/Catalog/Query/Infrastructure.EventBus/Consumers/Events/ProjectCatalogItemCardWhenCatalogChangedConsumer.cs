using Application.UseCases.Events;
using Contracts.Services.Catalog;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectCatalogItemCardWhenCatalogChangedConsumer : IConsumer<DomainEvent.CatalogItemAdded>
{
    private readonly IProjectCatalogItemCardWhenCatalogChangedInteractor _interactor;

    public ProjectCatalogItemCardWhenCatalogChangedConsumer(IProjectCatalogItemCardWhenCatalogChangedInteractor interactor)
    {
        _interactor = interactor;
    }

    public Task Consume(ConsumeContext<DomainEvent.CatalogItemAdded> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);
}