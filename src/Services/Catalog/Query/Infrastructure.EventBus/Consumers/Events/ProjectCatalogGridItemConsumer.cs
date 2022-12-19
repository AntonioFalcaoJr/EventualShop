using Application.Abstractions;
using Contracts.Services.Catalog;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectCatalogGridItemConsumer :
    IConsumer<DomainEvent.CatalogActivated>,
    IConsumer<DomainEvent.CatalogCreated>,
    IConsumer<DomainEvent.CatalogDeactivated>,
    IConsumer<DomainEvent.CatalogDeleted>,
    IConsumer<DomainEvent.CatalogDescriptionChanged>,
    IConsumer<DomainEvent.CatalogTitleChanged>
{
    private readonly IServiceProvider _serviceProvider;

    public ProjectCatalogGridItemConsumer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task Consume(ConsumeContext<DomainEvent.CatalogActivated> context)
        => _serviceProvider
            .GetRequiredService<IInteractor<DomainEvent.CatalogActivated>>()
            .InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CatalogCreated> context)
        => _serviceProvider
            .GetRequiredService<IInteractor<DomainEvent.CatalogCreated>>()
            .InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CatalogDeactivated> context)
        => _serviceProvider
            .GetRequiredService<IInteractor<DomainEvent.CatalogDeactivated>>()
            .InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CatalogDeleted> context)
        => _serviceProvider
            .GetRequiredService<IInteractor<DomainEvent.CatalogDeleted>>()
            .InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CatalogDescriptionChanged> context)
        => _serviceProvider
            .GetRequiredService<IInteractor<DomainEvent.CatalogDescriptionChanged>>()
            .InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CatalogTitleChanged> context)
        => _serviceProvider
            .GetRequiredService<IInteractor<DomainEvent.CatalogTitleChanged>>()
            .InteractAsync(context.Message, context.CancellationToken);
}