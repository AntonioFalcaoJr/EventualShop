using Application.UseCases.EventHandlers.Integrations;
using Application.UseCases.EventHandlers.Projections;
using ECommerce.Abstractions.Messages.Events;
using ECommerce.Contracts.Accounts;
using MassTransit;

namespace Infrastructure.MessageBus.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistrationContext registration)
    {
        cfg.ConfigureEventReceiveEndpoint<CreateAccountWhenUserRegisteredConsumer, ECommerce.Contracts.Identities.DomainEvent.UserRegistered>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectAccountDetailsWhenChangedConsumer, DomainEvent.ProfessionalAddressDefined>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectAccountDetailsWhenChangedConsumer, DomainEvent.ProfileUpdated>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectAccountDetailsWhenChangedConsumer, DomainEvent.ResidenceAddressDefined>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectAccountDetailsWhenChangedConsumer, DomainEvent.AccountCreated>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectAccountDetailsWhenChangedConsumer, DomainEvent.AccountDeleted>(registration);
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