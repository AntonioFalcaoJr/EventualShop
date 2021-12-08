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
        cfg.AddCommandConsumer<CreateAccountConsumer, Commands.CreateAccount>();
        cfg.AddCommandConsumer<DefineProfessionalAddressConsumer, Commands.DefineProfessionalAddress>();
        cfg.AddCommandConsumer<DefineResidenceAddressConsumer, Commands.DefineResidenceAddress>();
        cfg.AddCommandConsumer<DeleteAccountConsumer, Commands.DeleteAccount>();
        cfg.AddCommandConsumer<UpdateProfileConsumer, Commands.UpdateProfile>();
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