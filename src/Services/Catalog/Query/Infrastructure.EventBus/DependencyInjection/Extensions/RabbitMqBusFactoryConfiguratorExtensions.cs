using Contracts.Abstractions.Messages;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Consumers.Events;
using MassTransit;

namespace Infrastructure.EventBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext context)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectDetailsWhenCatalogCreatedConsumer, DomainEvent.CatalogCreated>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectDetailsWhenCatalogDeletedConsumer, DomainEvent.CatalogDeleted>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectListItemWhenCatalogDeletedConsumer, DomainEvent.CatalogDeleted>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectDetailsWhenCatalogDeactivatedConsumer, DomainEvent.CatalogDeactivated>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectDetailsWhenCatalogActivatedConsumer, DomainEvent.CatalogActivated>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectDetailsWhenCatalogDescriptionChangedConsumer, DomainEvent.CatalogDescriptionChanged>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectDetailsWhenCatalogTitleChangedConsumer, DomainEvent.CatalogTitleChanged>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectListItemWhenCatalogItemAddedConsumer, DomainEvent.CatalogItemAdded>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectListItemWhenCatalogItemRemovedConsumer, DomainEvent.CatalogItemRemoved>(context);
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