using Application.UseCases.Events.Integrations;
using Application.UseCases.Events.Projections;
using Contracts.Abstractions;
using Contracts.Services.ShoppingCart;
using MassTransit;

namespace Infrastructure.MessageBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext registration)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvent.CartCreated>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvent.CartItemAdded>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvent.CartItemRemoved>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvent.CreditCardAdded>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvent.PayPalAdded>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvent.BillingAddressChanged>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvent.ShippingAddressAdded>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvent.CartCheckedOut>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvent.CartItemIncreased>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvent.CartItemDecreased>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvent.CartDiscarded>(registration);

        cfg.ConfigureEventReceiveEndpoint<ProjectCartItemsWhenChangedConsumer, DomainEvent.CartItemAdded>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartItemsWhenChangedConsumer, DomainEvent.CartItemDecreased>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartItemsWhenChangedConsumer, DomainEvent.CartItemIncreased>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartItemsWhenChangedConsumer, DomainEvent.CartItemRemoved>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartItemsWhenChangedConsumer, DomainEvent.CartDiscarded>(registration);

        cfg.ConfigureEventReceiveEndpoint<PublishCartSubmittedWhenCheckedOutConsumer, DomainEvent.CartCheckedOut>(registration);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistrationContext registration)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"shopping-cart.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TEvent).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Bind<TEvent>();
                endpoint.ConfigureConsumer<TConsumer>(registration);
            });
}