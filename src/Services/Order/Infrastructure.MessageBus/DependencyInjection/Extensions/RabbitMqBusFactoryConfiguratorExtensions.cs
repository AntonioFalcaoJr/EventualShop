using Application.UseCases.EventHandlers.Integrations;
using Application.UseCases.EventHandlers.Projections;
using ECommerce.Abstractions.Messages.Events;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;
using DomainEvent = ECommerce.Contracts.Orders.DomainEvent;

namespace Infrastructure.MessageBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext registration)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectOrderDetailsWhenOrderChangedConsumer, DomainEvent.OrderPlaced>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectOrderDetailsWhenOrderChangedConsumer, DomainEvent.OrderConfirmed>(registration);
        cfg.ConfigureEventReceiveEndpoint<PlaceOrderWhenCartSubmittedConsumer, IntegrationEvent.CartSubmitted>(registration);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistrationContext registration)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"order.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TEvent).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Bind<TEvent>();
                endpoint.ConfigureConsumer<TConsumer>(registration);
            });
}