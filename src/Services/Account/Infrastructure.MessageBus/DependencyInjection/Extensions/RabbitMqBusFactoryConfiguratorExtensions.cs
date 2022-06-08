using Application.UseCases.Events.Projections;
using Contracts.Abstractions.Messages;
using Contracts.Services.Account;
using MassTransit;

namespace Infrastructure.MessageBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext registration)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectAccountWhenChangedConsumer, DomainEvent.AccountCreated>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectAccountWhenChangedConsumer, DomainEvent.AccountDeleted>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectAddressWhenChangedConsumer, DomainEvent.ShippingAddressAdded>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectAddressWhenChangedConsumer, DomainEvent.BillingAddressAdded>(registration);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistrationContext registration)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"account.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TEvent).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Bind<TEvent>();
                endpoint.ConfigureConsumer<TConsumer>(registration);
            });
}