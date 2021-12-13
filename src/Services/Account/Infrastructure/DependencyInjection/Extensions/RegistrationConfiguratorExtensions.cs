using System;
using Application.UseCases.Commands;
using Application.UseCases.Events.Integrations;
using Application.UseCases.Events.Projections;
using Application.UseCases.Queries;
using ECommerce.Abstractions.Commands;
using ECommerce.Contracts.Account;
using MassTransit;

namespace Infrastructure.DependencyInjection.Extensions;

public static class RegistrationConfiguratorExtensions
{
    public static void AddCommandConsumers(this IRegistrationConfigurator cfg)
    {
        cfg.AddCommandConsumer<CreateAccountConsumer>();
        cfg.AddCommandConsumer<DefineProfessionalAddressConsumer>();
        cfg.AddCommandConsumer<DefineResidenceAddressConsumer>();
        cfg.AddCommandConsumer<DeleteAccountConsumer>();
        cfg.AddCommandConsumer<UpdateProfileConsumer>();
    }

    public static void AddEventConsumers(this IRegistrationConfigurator cfg)
    {
        cfg.AddConsumer<CreateAccountWhenUserRegisteredConsumer>();
        cfg.AddConsumer<ProjectAccountDetailsWhenAccountChangedConsumer>();
    }

    public static void AddQueryConsumers(this IRegistrationConfigurator cfg)
    {
        cfg.AddConsumer<GetAccountDetailsConsumer>();
        cfg.AddConsumer<GetAccountsDetailsWithPaginationConsumer>();
    }

    private static void AddCommandConsumer<TConsumer>(this IRegistrationConfigurator cfg)
        where TConsumer : class, IConsumer
        => cfg
            .AddConsumer<TConsumer>()
            .Endpoint(endpoint => endpoint.ConfigureConsumeTopology = false);
}