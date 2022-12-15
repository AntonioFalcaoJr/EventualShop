using Contracts.Abstractions.Messages;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Consumers.Events;
using MassTransit;

namespace Infrastructure.EventBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext context)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogGridItemWhenCatalogCreatedConsumer, DomainEvent.CatalogCreated>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogGridItemWhenCatalogDeletedConsumer, DomainEvent.CatalogDeleted>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogItemListItemWhenCatalogDeletedConsumer, DomainEvent.CatalogDeleted>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogGridItemWhenCatalogDeactivatedConsumer, DomainEvent.CatalogDeactivated>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogGridItemWhenCatalogActivatedConsumer, DomainEvent.CatalogActivated>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogGridItemWhenCatalogDescriptionChangedConsumer, DomainEvent.CatalogDescriptionChanged>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogGridItemWhenCatalogTitleChangedConsumer, DomainEvent.CatalogTitleChanged>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogItemListItemWhenCatalogItemAddedConsumer, DomainEvent.CatalogItemAdded>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogItemCardWhenCatalogItemAddedConsumer, DomainEvent.CatalogItemAdded>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogItemDetailsWhenCatalogItemAddedConsumer, DomainEvent.CatalogItemAdded>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogItemListItemWhenCatalogItemRemovedConsumer, DomainEvent.CatalogItemRemoved>(context);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistrationContext context)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"catalog.query.{typeof(TConsumer).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Bind<TEvent>();
                endpoint.ConfigureConsumer<TConsumer>(context);
            });
}