using System;
using Application.UseCases.Events.Integrations;
using Application.UseCases.Events.Projections;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Messages.Abstractions.Events;
using Messages.Services.ShoppingCarts;

namespace Infrastructure.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistration registration)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenCartChangedConsumer, DomainEvents.CartCreated>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenCartChangedConsumer, DomainEvents.CartItemAdded>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenCartChangedConsumer, DomainEvents.CartItemRemoved>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenCartChangedConsumer, DomainEvents.CreditCardAdded>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenCartChangedConsumer, DomainEvents.PayPalAdded>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenCartChangedConsumer, DomainEvents.BillingAddressChanged>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenCartChangedConsumer, DomainEvents.ShippingAddressAdded>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenCartChangedConsumer, DomainEvents.CartCheckedOut>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenCartChangedConsumer, DomainEvents.CartItemQuantityIncreased>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenCartChangedConsumer, DomainEvents.CartItemQuantityUpdated>(registration);
        cfg.ConfigureEventReceiveEndpoint<PublishCartSubmittedWhenCartCheckedOutConsumer, DomainEvents.CartCheckedOut>(registration);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TMessage>(this IRabbitMqBusFactoryConfigurator bus, IRegistration registration)
        where TConsumer : class, IConsumer
        where TMessage : class, IEvent
    {
        bus.ReceiveEndpoint(
            queueName: $"shoppingcart-{typeof(TConsumer).ToKebabCaseString()}-{typeof(TMessage).ToKebabCaseString()}",
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