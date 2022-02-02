using Application.UseCases.Events.Integrations;
using Application.UseCases.Events.Projections;
using ECommerce.Abstractions.Events;
using ECommerce.Contracts.ShoppingCart;
using MassTransit;
using MassTransit.Context;
using MassTransit.RabbitMqTransport;
using DomainEvents = ECommerce.Contracts.Order.DomainEvents;

namespace Infrastructure.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistration registration)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectOrderDetailsWhenOrderChangedConsumer, DomainEvents.OrderPlaced>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectOrderDetailsWhenOrderChangedConsumer, DomainEvents.OrderConfirmed>(registration);
        cfg.ConfigureEventReceiveEndpoint<PlaceOrderWhenCartSubmittedConsumer, IntegrationEvents.CartSubmitted>(registration);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistration registration)
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