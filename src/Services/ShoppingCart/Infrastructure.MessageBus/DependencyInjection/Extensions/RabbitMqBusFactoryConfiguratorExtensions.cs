using Application.UseCases.Events.Integrations;
using Application.UseCases.Events.Projections;
using Contracts.Abstractions.Messages;
using Contracts.Services.ShoppingCart;
using MassTransit;

namespace Infrastructure.MessageBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext context)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvent.CartCreated>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvent.CartItemAdded>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvent.CartItemRemoved>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvent.BillingAddressAdded>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvent.ShippingAddressAdded>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvent.CartCheckedOut>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvent.CartItemIncreased>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvent.CartItemDecreased>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartWhenChangedConsumer, DomainEvent.CartDiscarded>(context);

        cfg.ConfigureEventReceiveEndpoint<ProjectCartItemsWhenChangedConsumer, DomainEvent.CartItemAdded>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartItemsWhenChangedConsumer, DomainEvent.CartItemDecreased>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartItemsWhenChangedConsumer, DomainEvent.CartItemIncreased>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartItemsWhenChangedConsumer, DomainEvent.CartItemRemoved>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectCartItemsWhenChangedConsumer, DomainEvent.CartDiscarded>(context);
        
        cfg.ConfigureEventReceiveEndpoint<ProjectCartPaymentMethodsWhenChangedConsumer, DomainEvent.PaymentMethodAdded>(context);

        cfg.ConfigureEventReceiveEndpoint<PublishCartSubmittedWhenCheckedOutConsumer, DomainEvent.CartCheckedOut>(context);
        
        cfg.ConfigureEventReceiveEndpoint<ConfirmItemWhenInventoryReservedConsumer, Contracts.Services.Warehouse.DomainEvent.InventoryReserved>(context);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistrationContext context)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"shopping-cart.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TEvent).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Bind<TEvent>();
                endpoint.ConfigureConsumer<TConsumer>(context);
            });
}