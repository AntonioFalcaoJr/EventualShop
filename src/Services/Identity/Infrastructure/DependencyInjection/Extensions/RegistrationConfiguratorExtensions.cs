using System;
using Application.UseCases.Commands;
using Application.UseCases.Events.Projections;
using Application.UseCases.Queries;
using ECommerce.Abstractions.Commands;
using ECommerce.Contracts.Identity;
using MassTransit;

namespace Infrastructure.DependencyInjection.Extensions;

public static class RegistrationConfiguratorExtensions
{
    public static void AddCommandConsumers(this IRegistrationConfigurator cfg)
    {
        cfg.AddCommandConsumer<RegisterUserConsumer>();
        cfg.AddCommandConsumer<ChangeUserPasswordConsumer>();
        cfg.AddCommandConsumer<DeleteUserConsumer>();
    }

    public static void AddEventConsumers(this IRegistrationConfigurator cfg)
    {
        cfg.AddConsumer<ProjectUserDetailsWhenUserChangedConsumer>();
    }

    public static void AddQueryConsumers(this IRegistrationConfigurator cfg)
    {
        cfg.AddConsumer<GetUserAuthenticationDetailsConsumer>();
    }

    private static void AddCommandConsumer<TConsumer>(this IRegistrationConfigurator cfg)
        where TConsumer : class, IConsumer
        => cfg
            .AddConsumer<TConsumer>()
            .Endpoint(endpoint => endpoint.ConfigureConsumeTopology = false);
}