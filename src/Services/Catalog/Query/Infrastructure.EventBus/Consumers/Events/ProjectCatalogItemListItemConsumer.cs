using Application.Abstractions;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.DependencyInjection.Providers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectCatalogItemListItemConsumer :
    IConsumer<DomainEvent.CatalogDeleted>,
    IConsumer<DomainEvent.CatalogItemAdded>,
    IConsumer<DomainEvent.CatalogItemRemoved>
{
    private readonly ILazyInteractorProvider _lazyInteractorProvider;

    public ProjectCatalogItemListItemConsumer(ILazyInteractorProvider lazyInteractorProvider)
    {
        _lazyInteractorProvider = lazyInteractorProvider;
    }

    public Task Consume(ConsumeContext<DomainEvent.CatalogDeleted> context)
        => _lazyInteractorProvider.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CatalogItemAdded> context)
        => _lazyInteractorProvider.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CatalogItemRemoved> context)
        => _lazyInteractorProvider.InteractAsync(context.Message, context.CancellationToken);
}