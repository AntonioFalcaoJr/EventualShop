using Application.UseCases.Events;
using ECommerce.Abstractions.Messages.Events;
using ECommerce.Contracts.Catalog;
using MassTransit;

namespace Infrastructure.MessageBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext registration)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogDetailsWhenChangedConsumer, DomainEvents.CatalogCreated>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogDetailsWhenChangedConsumer, DomainEvents.CatalogDeleted>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogDetailsWhenChangedConsumer, DomainEvents.CatalogActivated>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogDetailsWhenChangedConsumer, DomainEvents.CatalogDeactivated>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogDetailsWhenChangedConsumer, DomainEvents.CatalogUpdated>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogDetailsWhenChangedConsumer, DomainEvents.CatalogItemAdded>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogDetailsWhenChangedConsumer, DomainEvents.CatalogItemRemoved>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogDetailsWhenChangedConsumer, DomainEvents.CatalogItemUpdated>(registration);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistrationContext registration)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"catalog.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TEvent).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Bind<TEvent>();
                endpoint.ConfigureConsumer<TConsumer>(registration);
            });
}