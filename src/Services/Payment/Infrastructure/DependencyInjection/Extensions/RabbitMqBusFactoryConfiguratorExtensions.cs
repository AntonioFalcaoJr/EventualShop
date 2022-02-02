using Application.UseCases.Events.Behaviors;
using Application.UseCases.Events.Integrations;
using Application.UseCases.Events.Projections;
using ECommerce.Abstractions.Events;
using ECommerce.Contracts.Payment;
using MassTransit;
using MassTransit.Context;
using MassTransit.RabbitMqTransport;

namespace Infrastructure.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistration registration)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectPaymentDetailsWhenPaymentChangedConsumer, DomainEvents.PaymentCanceled>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProceedWithPaymentWhenPaymentRequestedConsumer, DomainEvents.PaymentRequested>(registration);
        cfg.ConfigureEventReceiveEndpoint<RequestPaymentWhenOrderPlacedConsumer, ECommerce.Contracts.Order.DomainEvents.OrderPlaced>(registration);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistration registration)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"payment.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TEvent).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Bind<TEvent>();
                endpoint.ConfigureConsumer<TConsumer>(registration);
            });
}