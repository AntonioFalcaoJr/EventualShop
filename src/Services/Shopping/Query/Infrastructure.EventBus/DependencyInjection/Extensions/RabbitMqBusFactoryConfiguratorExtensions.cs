using Contracts.Abstractions.Messages;
using Contracts.Boundaries.Shopping.ShoppingCart;
using Infrastructure.EventBus.Consumers.Events;
using MassTransit;

namespace Infrastructure.EventBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IBusFactoryConfigurator cfg, IRegistrationContext context)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenChangedConsumer, DomainEvent.ShoppingStarted>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenChangedConsumer, DomainEvent.CartDiscarded>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenChangedConsumer, DomainEvent.CartCheckedOut>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenChangedConsumer, DomainEvent.CartItemAdded>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenChangedConsumer, DomainEvent.CartItemDecreased>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenChangedConsumer, DomainEvent.CartItemIncreased>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenChangedConsumer, DomainEvent.CartItemRemoved>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartDetailsWhenChangedConsumer, SummaryEvent.CartProjectionRebuilt>(context);

        cfg.ConfigureEventReceiveEndpoint<ProjectCartItemListItemWhenCartChangedConsumer, DomainEvent.CartItemAdded>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartItemListItemWhenCartChangedConsumer, DomainEvent.CartItemRemoved>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartItemListItemWhenCartChangedConsumer, DomainEvent.CartItemIncreased>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartItemListItemWhenCartChangedConsumer, DomainEvent.CartDiscarded>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartItemListItemWhenCartChangedConsumer, DomainEvent.CartItemDecreased>(context);

        // cfg.ConfigureEventReceiveEndpoint<ProjectPaymentMethodListItemWhenCartChangedConsumer, DomainEvent.CreditCardAdded>(context);
        // cfg.ConfigureEventReceiveEndpoint<ProjectPaymentMethodListItemWhenCartChangedConsumer, DomainEvent.DebitCardAdded>(context);
        // cfg.ConfigureEventReceiveEndpoint<ProjectPaymentMethodListItemWhenCartChangedConsumer, DomainEvent.PayPalAdded>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectPaymentMethodListItemWhenCartChangedConsumer, DomainEvent.CartDiscarded>(context);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IReceiveConfigurator bus, IRegistrationContext context)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"shopping.query.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TEvent).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                if (endpoint is IRabbitMqReceiveEndpointConfigurator rabbitMq) rabbitMq.Bind<TEvent>();
                if (endpoint is IInMemoryReceiveEndpointConfigurator inMemory) inMemory.Bind<TEvent>();

                endpoint.ConfigureConsumeTopology = false;
                endpoint.ConfigureConsumer<TConsumer>(context);
            });
}