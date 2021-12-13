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
        cfg.AddCommandConsumer<PlaceOrderConsumer>();
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

    private static void AddCommandConsumer<TConsumer>(this IRegistrationConfigurator cfg)
        where TConsumer : class, IConsumer
        => cfg
            .AddConsumer<TConsumer>()
            .Endpoint(endpoint => endpoint.ConfigureConsumeTopology = false);
}