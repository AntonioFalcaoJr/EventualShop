using Application.UseCases.EventHandlers;
using Domain.Abstractions.Events;
using Domain.Entities.Catalogs;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace Infrastructure.DependencyInjection.Extensions
{
    internal static class RabbitMqBusConfiguratorExtensions
    {
        public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistration registration)
        {
            cfg.ConfigureEventReceiveEndpoint<CatalogCreatedConsumer, Events.CatalogCreated>(registration);
            cfg.ConfigureEventReceiveEndpoint<CatalogChangedConsumer, Events.CatalogDeleted>(registration);
            cfg.ConfigureEventReceiveEndpoint<CatalogChangedConsumer, Events.CatalogActivated>(registration);
            cfg.ConfigureEventReceiveEndpoint<CatalogChangedConsumer, Events.CatalogDeactivated>(registration);
            cfg.ConfigureEventReceiveEndpoint<CatalogChangedConsumer, Events.CatalogUpdated>(registration);
            cfg.ConfigureEventReceiveEndpoint<CatalogChangedConsumer, Events.CatalogItemAdded>(registration);
            cfg.ConfigureEventReceiveEndpoint<CatalogChangedConsumer, Events.CatalogItemRemoved>(registration);
            cfg.ConfigureEventReceiveEndpoint<CatalogChangedConsumer, Events.CatalogItemEdited>(registration);
        }

        private static void ConfigureEventReceiveEndpoint<TConsumer, TMessage>(this IRabbitMqBusFactoryConfigurator cfg, IRegistration registration)
            where TConsumer : class, IConsumer
            where TMessage : class, IDomainEvent
        {
            cfg.ReceiveEndpoint(
                queueName: typeof(TMessage).ToKebabCaseString(),
                configureEndpoint: endpoint =>
                {
                    endpoint.ConfigureConsumer<TConsumer>(registration);
                    endpoint.ConfigureConsumeTopology = false;
                });
        }
    }
}