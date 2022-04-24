using Application.UseCases.EventHandlers.Projections;
using ECommerce.Abstractions.Messages.Events;
using ECommerce.Contracts.Identities;
using MassTransit;

namespace Infrastructure.MessageBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext registration)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectUserDetailsWhenUserChangedConsumer, DomainEvent.UserRegistered>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectUserDetailsWhenUserChangedConsumer, DomainEvent.UserPasswordChanged>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectUserDetailsWhenUserChangedConsumer, DomainEvent.UserDeleted>(registration);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistrationContext registration)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"identity.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TEvent).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Bind<TEvent>();
                endpoint.ConfigureConsumer<TConsumer>(registration);
            });
}