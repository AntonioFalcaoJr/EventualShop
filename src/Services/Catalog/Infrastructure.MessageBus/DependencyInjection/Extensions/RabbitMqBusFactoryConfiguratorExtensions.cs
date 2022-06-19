using Application.UseCases.Events;
using Contracts.Abstractions.Messages;
using Contracts.Services.Catalog;
using MassTransit;

namespace Infrastructure.MessageBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext context)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogWhenChangedConsumer, DomainEvent.CatalogCreated>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogWhenChangedConsumer, DomainEvent.CatalogDeleted>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogWhenChangedConsumer, DomainEvent.CatalogActivated>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogWhenChangedConsumer, DomainEvent.CatalogDeactivated>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogWhenChangedConsumer, DomainEvent.CatalogDescriptionChanged>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogWhenChangedConsumer, DomainEvent.CatalogTitleChanged>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogWhenChangedConsumer, DomainEvent.CatalogItemAdded>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCatalogWhenChangedConsumer, DomainEvent.CatalogItemRemoved>(context);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistrationContext context)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"catalog.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TEvent).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Bind<TEvent>();
                endpoint.ConfigureConsumer<TConsumer>(context);
            });
}