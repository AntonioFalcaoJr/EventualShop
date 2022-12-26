using Application.Abstractions;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.DependencyInjection.Providers;
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
    private readonly ILazyInteractorProvider _lazyInteractorProvider;

    public ProjectCatalogGridItemConsumer(ILazyInteractorProvider lazyInteractorProvider)
    {
        _lazyInteractorProvider = lazyInteractorProvider;
    }

    public Task Consume(ConsumeContext<DomainEvent.CatalogActivated> context)
        => _lazyInteractorProvider.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CatalogCreated> context)
        => _lazyInteractorProvider.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CatalogDeactivated> context)
        => _lazyInteractorProvider.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CatalogDeleted> context)
        => _lazyInteractorProvider.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CatalogDescriptionChanged> context)
        => _lazyInteractorProvider.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CatalogTitleChanged> context)
        => _lazyInteractorProvider.InteractAsync(context.Message, context.CancellationToken);
}