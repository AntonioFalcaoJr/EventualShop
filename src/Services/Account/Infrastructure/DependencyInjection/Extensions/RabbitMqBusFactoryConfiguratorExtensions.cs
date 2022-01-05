using System;
using Application.UseCases.Events.Integrations;
using Application.UseCases.Events.Projections;
using ECommerce.Abstractions.Events;
using ECommerce.Contracts.Account;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace Infrastructure.DependencyInjection.Extensions;

internal static class RabbitMqBusFactoryConfiguratorExtensions
{
    public static void ConfigureEventReceiveEndpoints(this IRabbitMqBusFactoryConfigurator cfg, IRegistration registration)
    {
        cfg.ConfigureEventReceiveEndpoint<CreateAccountWhenUserRegisteredConsumer, ECommerce.Contracts.Identity.DomainEvents.UserRegistered>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectAccountDetailsWhenAccountChangedConsumer, DomainEvents.ProfessionalAddressDefined>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectAccountDetailsWhenAccountChangedConsumer, DomainEvents.ProfileUpdated>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectAccountDetailsWhenAccountChangedConsumer, DomainEvents.ResidenceAddressDefined>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectAccountDetailsWhenAccountChangedConsumer, DomainEvents.AccountCreated>(registration);
        cfg.ConfigureEventReceiveEndpoint<ProjectAccountDetailsWhenAccountChangedConsumer, DomainEvents.AccountDeleted>(registration);
    }

    private static void ConfigureEventReceiveEndpoint<TConsumer, TEvent>(this IRabbitMqBusFactoryConfigurator bus, IRegistration registration)
        where TConsumer : class, IConsumer
        where TEvent : class, IEvent
        => bus.ReceiveEndpoint(
            queueName: $"account.{typeof(TConsumer).ToKebabCaseString()}.{typeof(TEvent).ToKebabCaseString()}",
            configureEndpoint: endpoint =>
            {
                endpoint.ConfigureConsumeTopology = false;

                endpoint.ConfigureConsumer<TConsumer>(registration);
                endpoint.Bind<TEvent>();

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