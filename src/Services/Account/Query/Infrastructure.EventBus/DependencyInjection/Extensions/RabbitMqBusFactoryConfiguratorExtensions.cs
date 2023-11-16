using Contracts.Abstractions.Messages;
using Contracts.Boundaries.Account;
using Infrastructure.EventBus.Consumers.Events;
using MassTransit;

namespace Infrastructure.EventBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext context)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectAccountDetailsWhenAccountChangedInteractor, DomainEvent.AccountCreated>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectAccountDetailsWhenAccountChangedInteractor, DomainEvent.AccountActivated>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectAccountDetailsWhenAccountChangedInteractor, DomainEvent.AccountDeleted>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectAccountDetailsWhenAccountChangedInteractor, DomainEvent.AccountDeactivated>(context);

        cfg.ConfigureEventReceiveEndpoint<ProjectBillingAddressListItemWhenAccountChangedConsumer, DomainEvent.BillingAddressAdded>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectBillingAddressListItemWhenAccountChangedConsumer, DomainEvent.AccountDeleted>(context);

        cfg.ConfigureEventReceiveEndpoint<ProjectShippingAddressListItemWhenAccountChangedConsumer, DomainEvent.ShippingAddressAdded>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectShippingAddressListItemWhenAccountChangedConsumer, DomainEvent.AccountDeleted>(context);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistrationContext context)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"account.query.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TEvent).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Bind<TEvent>();
                endpoint.ConfigureConsumer<TConsumer>(context);
            });
}