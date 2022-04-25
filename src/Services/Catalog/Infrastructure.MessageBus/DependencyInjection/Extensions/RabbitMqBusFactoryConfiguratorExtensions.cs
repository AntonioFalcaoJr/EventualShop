using Application.UseCases.Events;
using ECommerce.Abstractions;
using ECommerce.Contracts.Catalogs;
using MassTransit;

namespace Infrastructure.MessageBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext registration)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogWhenChangedConsumer, DomainEvent.CatalogCreated>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogWhenChangedConsumer, DomainEvent.CatalogDeleted>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogWhenChangedConsumer, DomainEvent.CatalogActivated>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogWhenChangedConsumer, DomainEvent.CatalogDeactivated>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogWhenChangedConsumer, DomainEvent.CatalogDescriptionChanged>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogWhenChangedConsumer, DomainEvent.CatalogTitleChanged>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogWhenChangedConsumer, DomainEvent.CatalogItemAdded>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogWhenChangedConsumer, DomainEvent.CatalogItemRemoved>(registration);
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