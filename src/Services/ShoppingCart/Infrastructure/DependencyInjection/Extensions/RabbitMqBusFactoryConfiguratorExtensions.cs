using Application.UseCases.Events.Integrations;
using Application.UseCases.Events.Projections;
using ECommerce.Abstractions.Messages.Events;
using ECommerce.Contracts.ShoppingCart;
using MassTransit;

namespace Infrastructure.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext registration)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvents.CartCreated>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvents.CartItemAdded>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvents.CartItemRemoved>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvents.CreditCardAdded>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvents.PayPalAdded>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvents.BillingAddressChanged>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvents.ShippingAddressAdded>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvents.CartCheckedOut>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvents.CartItemIncreased>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvents.CartItemDecreased>(registration);
        
        cfg.ConfigureEventReceiveEndpoint<ProjectCartItemsWhenChangedConsumer, DomainEvents.CartItemAdded>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartItemsWhenChangedConsumer, DomainEvents.CartItemDecreased>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartItemsWhenChangedConsumer, DomainEvents.CartItemIncreased>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartItemsWhenChangedConsumer, DomainEvents.CartItemRemoved>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartItemsWhenChangedConsumer, DomainEvents.CartDiscarded>(registration);
        
        cfg.ConfigureEventReceiveEndpoint<PublishCartSubmittedWhenCartCheckedOutConsumer, DomainEvents.CartCheckedOut>(registration);
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