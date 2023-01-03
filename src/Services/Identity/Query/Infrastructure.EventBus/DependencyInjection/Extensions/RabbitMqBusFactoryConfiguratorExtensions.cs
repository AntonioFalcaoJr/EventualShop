using Contracts.Abstractions.Messages;
using Contracts.Services.Identity;
using Infrastructure.EventBus.Consumers;
using MassTransit;

namespace Infrastructure.EventBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext context)
    {
        cfg.ConfigureEventReceiveEndpoint<ProjectUserDetailsWhenUserChangedConsumer, DomainEvent.UserDeleted>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectUserDetailsWhenUserChangedConsumer, DomainEvent.UserRegistered>(context);
        cfg.ConfigureEventReceiveEndpoint<ProjectUserDetailsWhenUserChangedConsumer, DomainEvent.UserPasswordChanged>(context);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistrationContext context)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"identity.query.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TEvent).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;
                endpoint.Bind<TEvent>();
                endpoint.ConfigureConsumer<TConsumer>(context);
            });
}