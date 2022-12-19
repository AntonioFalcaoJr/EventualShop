using Application.Abstractions;
using Contracts.Services.Catalog;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectCatalogItemListItemConsumer :
    IConsumer<DomainEvent.CatalogDeleted>,
    IConsumer<DomainEvent.CatalogItemAdded>,
    IConsumer<DomainEvent.CatalogItemRemoved>
{
    private readonly IServiceProvider _serviceProvider;

    public ProjectCatalogItemListItemConsumer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task Consume(ConsumeContext<DomainEvent.CatalogDeleted> context)
        => _serviceProvider
            .GetRequiredService<IInteractor<DomainEvent.CatalogDeleted>>()
            .InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CatalogItemAdded> context)
        => _serviceProvider
            .GetRequiredService<IInteractor<DomainEvent.CatalogItemAdded>>()
            .InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CatalogItemRemoved> context)
        => _serviceProvider
            .GetRequiredService<IInteractor<DomainEvent.CatalogItemRemoved>>()
            .InteractAsync(context.Message, context.CancellationToken);
}