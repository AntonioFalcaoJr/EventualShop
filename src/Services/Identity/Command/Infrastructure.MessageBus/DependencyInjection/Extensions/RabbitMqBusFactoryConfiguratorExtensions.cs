﻿using Contracts.Abstractions.Messages;
using Infrastructure.MessageBus.Consumers.Events;
using MassTransit;
using Identity = Contracts.Services.Identity;
using Account = Contracts.Services.Account;

namespace Infrastructure.MessageBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext context)
    {
        ConfigureEventReceiveEndpoint<AccountDeactivatedConsumer, Account.DomainEvent.AccountDeactivated>(cfg, context);
        ConfigureEventReceiveEndpoint<AccountDeletedConsumer, Account.DomainEvent.AccountDeleted>(cfg, context);
        ConfigureEventReceiveEndpoint<EmailConfirmationExpiredConsumer, Identity.DelayedEvent.EmailConfirmationExpired>(cfg, context);
        ConfigureEventReceiveEndpoint<EmailConfirmedConsumer, Identity.DomainEvent.EmailConfirmed>(cfg, context);
        ConfigureEventReceiveEndpoint<UserRegisteredConsumer, Identity.DomainEvent.UserRegistered>(cfg, context);
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