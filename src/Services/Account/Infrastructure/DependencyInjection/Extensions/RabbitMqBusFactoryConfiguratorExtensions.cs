using System;
using Application.UseCases.Events.Integrations;
using Application.UseCases.Events.Projections;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Messages.Abstractions.Events;
using Messages.Services.Accounts;

namespace Infrastructure.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistration registration)
    {
        cfg.ConfigureEventReceiveEndpoint<CreateAccountWhenUserRegisteredConsumer, Messages.Services.Identities.DomainEvents.UserRegistered>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectAccountDetailsWhenAccountChangedConsumer, DomainEvents.ProfessionalAddressDefined>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectAccountDetailsWhenAccountChangedConsumer, DomainEvents.ProfileUpdated>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectAccountDetailsWhenAccountChangedConsumer, DomainEvents.ResidenceAddressDefined>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectAccountDetailsWhenAccountChangedConsumer, DomainEvents.AccountCreated>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectAccountDetailsWhenAccountChangedConsumer, DomainEvents.AccountDeleted>(registration);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TMessage>(this IRabbitMqBusFactoryConfigurator bus, IRegistration registration)
        where TConsumer : class, IConsumer
        where TMessage : class, IEvent
    {
        bus.ReceiveEndpoint(
            queueName: $"account-{typeof(TMessage).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;

                endpoint.ConfigureConsumer<TConsumer>(registration);
                endpoint.Bind<TMessage>();

                endpoint.UseCircuitBreaker(circuitBreaker => // TODO - Options
                {
                    circuitBreaker.TripThreshold = 15;
                    circuitBreaker.ResetInterval = TimeSpan.FromMinutes(3);
                    circuitBreaker.TrackingPeriod = TimeSpan.FromMinutes(1);
                    circuitBreaker.ActiveThreshold = 10;
                });

                endpoint.UseRateLimit(100, TimeSpan.FromSeconds(1)); // TODO - Options
            });
    }
}