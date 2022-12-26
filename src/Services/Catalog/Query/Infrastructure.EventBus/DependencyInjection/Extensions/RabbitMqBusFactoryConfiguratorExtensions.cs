using Contracts.Abstractions.Messages;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Consumers.Events;
using MassTransit;

namespace Infrastructure.EventBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext context)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogGridItemConsumer, DomainEvent.CatalogCreated>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogGridItemConsumer, DomainEvent.CatalogDeleted>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogGridItemConsumer, DomainEvent.CatalogDeactivated>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogGridItemConsumer, DomainEvent.CatalogActivated>(context);

        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogItemListItemConsumer, DomainEvent.CatalogDescriptionChanged>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogItemListItemConsumer, DomainEvent.CatalogTitleChanged>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogItemListItemConsumer, DomainEvent.CatalogDeleted>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogItemListItemConsumer, DomainEvent.CatalogItemAdded>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogItemListItemConsumer, DomainEvent.CatalogItemRemoved>(context);

        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogItemCardConsumer, DomainEvent.CatalogItemAdded>(context);

        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogItemDetailsConsumer, DomainEvent.CatalogItemAdded>(context);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistrationContext context)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"catalog.query.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TEvent).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Bind<TEvent>();
                endpoint.ConfigureConsumer<TConsumer>(context);
            });
}