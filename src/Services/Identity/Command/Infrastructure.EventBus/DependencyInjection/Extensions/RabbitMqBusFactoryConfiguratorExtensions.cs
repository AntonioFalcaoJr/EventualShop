using Contracts.Abstractions.Messages;
using Contracts.Boundaries.Identity;
using Infrastructure.EventBus.Consumers.Events;
using MassTransit;
using DomainEvent = Contracts.Boundaries.Account.DomainEvent;

namespace Infrastructure.EventBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext context)
    {
        ConfigureEventReceiveEndpoint<AccountDeactivatedConsumer, DomainEvent.AccountDeactivated>(cfg, context);
        ConfigureEventReceiveEndpoint<AccountDeletedConsumer, DomainEvent.AccountDeleted>(cfg, context);
        ConfigureEventReceiveEndpoint<EmailConfirmationExpiredConsumer, DelayedEvent.EmailConfirmationExpired>(cfg, context);
        ConfigureEventReceiveEndpoint<EmailConfirmedConsumer, Contracts.Boundaries.Identity.DomainEvent.EmailConfirmed>(cfg, context);
        ConfigureEventReceiveEndpoint<UserRegisteredConsumer, Contracts.Boundaries.Identity.DomainEvent.UserRegistered>(cfg, context);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistrationContext context)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"identity.command.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TEvent).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Bind<TEvent>();
                endpoint.ConfigureConsumer<TConsumer>(context);
            });
}