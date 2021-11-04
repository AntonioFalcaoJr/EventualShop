using System;
using Application.UseCases.Events;
using Application.UseCases.Events.Projections;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Messages.Abstractions.Events;
using Messages.Services.Payments;
using Orders = Messages.Services.Orders.Events;

namespace Infrastructure.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistration registration)
    {
        cfg.ConfigureEventReceiveEndpoint<PaymentRequestedConsumer, DomainEvents.PaymentRequested>(registration);
        cfg.ConfigureEventReceiveEndpoint<PaymentChangedConsumer, DomainEvents.PaymentCanceled>(registration);
        cfg.ConfigureEventReceiveEndpoint<OrderPlacedConsumer, Orders.OrderPlaced>(registration);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TMessage>(this IRabbitMqBusFactoryConfigurator bus, IRegistration registration)
        where TConsumer : class, IConsumer
        where TMessage : class, IEvent
    {
        bus.ReceiveEndpoint(
            queueName: $"payment-{typeof(TMessage).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;

                endpoint.ConfigureConsumer<TConsumer>(registration);
                endpoint.Bind<TMessage>();

                endpoint.UseCircuitBreaker(circuitBreaker => // TODO - Options
                {
                    circuitBreaker.TripThreshold = 15;
                    circuitBreaker.ResetInterval = TimeSpan.FromMinutes(3);
                    circuitBreaker.TrackingPeriod = TimeSpan.FromMinutes(1);
                    circuitBreaker.ActiveThreshold = 10;
                });

                endpoint.UseRateLimit(100, TimeSpan.FromSeconds(1)); // TODO - Options
            });
    }
}