using Contracts.Abstractions.Messages;
using Contracts.Services.Payment;
using Infrastructure.EventBus.Consumers.Events;
using MassTransit;

namespace Infrastructure.EventBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext context)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectPaymentMethodDetailsWhenChangedConsumer, DomainEvent.PaymentRequested>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectPaymentMethodDetailsWhenChangedConsumer, DomainEvent.PaymentMethodAuthorized>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectPaymentMethodDetailsWhenChangedConsumer, DomainEvent.PaymentMethodDenied>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectPaymentMethodDetailsWhenChangedConsumer, DomainEvent.PaymentMethodCanceled>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectPaymentMethodDetailsWhenChangedConsumer, DomainEvent.PaymentMethodCancellationDenied>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectPaymentMethodDetailsWhenChangedConsumer, DomainEvent.PaymentMethodRefunded>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectPaymentMethodDetailsWhenChangedConsumer, DomainEvent.PaymentMethodRefundDenied>(context);
        
        cfg.ConfigureEventReceiveEndpoint<ProjectPaymentDetailsWhenChangedConsumer, DomainEvent.PaymentRequested>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectPaymentDetailsWhenChangedConsumer, DomainEvent.PaymentCanceled>(context);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistrationContext context)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"payment.query-stack.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TEvent).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Bind<TEvent>();
                endpoint.ConfigureConsumer<TConsumer>(context);
            });
}