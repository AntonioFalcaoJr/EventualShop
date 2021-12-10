using System;
using Application.UseCases.Commands;
using Application.UseCases.Events.Integrations;
using Application.UseCases.Events.Projections;
using ECommerce.Abstractions.Commands;
using ECommerce.Contracts.ShoppingCart;
using MassTransit;

namespace Infrastructure.DependencyInjection.Extensions;

public static class RegistrationConfiguratorExtensions
{
    public static void AddCommandConsumers(this IRegistrationConfigurator cfg)
    {
        cfg.AddCommandConsumer<PlaceOrderConsumer, Commands.CreateCart>();
    }

    public static void AddEventConsumers(this IRegistrationConfigurator cfg)
    {
        cfg.AddConsumer<ProjectOrderDetailsWhenOrderChangedConsumer>();
        cfg.AddConsumer<PlaceOrderWhenCartSubmittedConsumer>();
    }

    public static void AddQueryConsumers(this IRegistrationConfigurator cfg)
    {
        // cfg.AddConsumer<GetUserAuthenticationDetailsConsumer>();
    }

    private static void AddCommandConsumer<TConsumer, TCommand>(this IRegistrationConfigurator configurator)
        where TConsumer : class, IConsumer
        where TCommand : class, ICommand
    {
        configurator
            .AddConsumer<TConsumer>()
            .Endpoint(endpoint => endpoint.ConfigureConsumeTopology = false);

        EndpointConvention.Map<TCommand>(new Uri($"exchange:{typeof(TCommand).ToKebabCaseString()}"));
    }
}