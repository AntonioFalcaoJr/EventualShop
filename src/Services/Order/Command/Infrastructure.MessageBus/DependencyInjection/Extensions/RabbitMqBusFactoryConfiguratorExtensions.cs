using Contracts.Abstractions.Messages;
using Infrastructure.MessageBus.Consumers.Events;
using MassTransit;
using ShoppingCart = Contracts.Services.ShoppingCart;
using Payment = Contracts.Services.Payment;

namespace Infrastructure.MessageBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext context)
    {
        ConfigureEventReceiveEndpoint<PlaceOrderWhenCartSubmittedConsumer, ShoppingCart.IntegrationEvent.CartSubmitted>(cfg, context);
        ConfigureEventReceiveEndpoint<ConfirmOrderWhenPaymentCompletedConsumer, Payment.DomainEvent.PaymentCompleted>(cfg, context);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistrationContext context)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"order.command-stack.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TEvent).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Bind<TEvent>();
                endpoint.ConfigureConsumer<TConsumer>(context);
            });
}