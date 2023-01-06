using Contracts.Abstractions.Messages;
using Contracts.Services.ShoppingCart;
using Infrastructure.EventBus.Consumers.Events;
using MassTransit;

namespace Infrastructure.EventBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext context)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenCartChangedConsumer, DomainEvent.BillingAddressAdded>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenCartChangedConsumer, DomainEvent.CartCreated>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenCartChangedConsumer, DomainEvent.CartDiscarded>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenCartChangedConsumer, DomainEvent.CartCheckedOut>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenCartChangedConsumer, DomainEvent.CartItemAdded>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenCartChangedConsumer, DomainEvent.CartItemDecreased>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenCartChangedConsumer, DomainEvent.CartItemIncreased>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenCartChangedConsumer, DomainEvent.CartItemRemoved>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenCartChangedConsumer, DomainEvent.ShippingAddressAdded>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenCartChangedConsumer, IntegrationEvent.ProjectionRebuilt>(context);

        cfg.ConfigureEventReceiveEndpoint<ProjectCartItemListItemWhenCartChangedConsumer, DomainEvent.CartItemAdded>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartItemListItemWhenCartChangedConsumer, DomainEvent.CartItemRemoved>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartItemListItemWhenCartChangedConsumer, DomainEvent.CartItemIncreased>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartItemListItemWhenCartChangedConsumer, DomainEvent.CartDiscarded>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartItemListItemWhenCartChangedConsumer, DomainEvent.CartItemDecreased>(context);

        cfg.ConfigureEventReceiveEndpoint<ProjectPaymentMethodListItemWhenCartChangedConsumer, DomainEvent.PaymentMethodAdded>(context);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistrationContext context)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"catalog.query-stack.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TEvent).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Bind<TEvent>();
                endpoint.ConfigureConsumer<TConsumer>(context);
            });
}