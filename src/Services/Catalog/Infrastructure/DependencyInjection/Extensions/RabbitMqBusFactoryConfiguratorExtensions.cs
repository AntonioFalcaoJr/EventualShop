using Application.UseCases.Events;
using ECommerce.Abstractions.Messages.Events;
using ECommerce.Contracts.Catalog;
using MassTransit;

namespace Infrastructure.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext registration)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogDetailsWhenCatalogChangedConsumer, DomainEvents.CatalogCreated>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogDetailsWhenCatalogChangedConsumer, DomainEvents.CatalogDeleted>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogDetailsWhenCatalogChangedConsumer, DomainEvents.CatalogActivated>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogDetailsWhenCatalogChangedConsumer, DomainEvents.CatalogDeactivated>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogDetailsWhenCatalogChangedConsumer, DomainEvents.CatalogUpdated>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogDetailsWhenCatalogChangedConsumer, DomainEvents.CatalogItemAdded>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogDetailsWhenCatalogChangedConsumer, DomainEvents.CatalogItemRemoved>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogDetailsWhenCatalogChangedConsumer, DomainEvents.CatalogItemUpdated>(registration);
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