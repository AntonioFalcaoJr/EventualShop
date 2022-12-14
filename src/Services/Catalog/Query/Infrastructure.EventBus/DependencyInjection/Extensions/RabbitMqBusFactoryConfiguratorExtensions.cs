using Contracts.Abstractions.Messages;
using Contracts.Services.Catalog;
using Infrastructure.EventBus.Consumers;
using Infrastructure.EventBus.Consumers.Events;
using MassTransit;

namespace Infrastructure.EventBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext context)
    {
        cfg.ConfigureEventReceiveEndpoint<CreateCatalogWhenCreatedConsumer, DomainEvent.CatalogCreated>(context);
        cfg.ConfigureEventReceiveEndpoint<DeleteCatalogWhenDeletedConsumer, DomainEvent.CatalogDeleted>(context);
        cfg.ConfigureEventReceiveEndpoint<DeleteCatalogItemWhenCatalogDeletedConsumer, DomainEvent.CatalogDeleted>(context);
        cfg.ConfigureEventReceiveEndpoint<DeactivateCatalogWhenDeactivatedConsumer, DomainEvent.CatalogDeactivated>(context);
        cfg.ConfigureEventReceiveEndpoint<ActivateCatalogWhenActivatedConsumer, DomainEvent.CatalogActivated>(context);
        cfg.ConfigureEventReceiveEndpoint<DescriptionChangeCatalogWhenChangedConsumer, DomainEvent.CatalogDescriptionChanged>(context);
        cfg.ConfigureEventReceiveEndpoint<TitleChangeCatalogWhenChangedConsumer, DomainEvent.CatalogTitleChanged>(context);
        cfg.ConfigureEventReceiveEndpoint<AddCatalogItemWhenAddedConsumer, DomainEvent.CatalogItemAdded>(context);
        cfg.ConfigureEventReceiveEndpoint<RemoveCatalogItemWhenRemovedConsumer, DomainEvent.CatalogItemRemoved>(context);
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