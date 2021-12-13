using System;
using Application.UseCases.Commands;
using Application.UseCases.Events.Behaviors;
using Application.UseCases.Events.Integrations;
using Application.UseCases.Events.Projections;
using Application.UseCases.Queries;
using ECommerce.Abstractions.Commands;
using ECommerce.Contracts.Payment;
using MassTransit;

namespace Infrastructure.DependencyInjection.Extensions;

public static class RegistrationConfiguratorExtensions
{
    public static void AddCommandConsumers(this IRegistrationConfigurator cfg)
    {
        cfg.AddCommandConsumer<RequestPaymentConsumer, Commands.RequestPayment>();
        cfg.AddCommandConsumer<CancelPaymentConsumer, Commands.CancelPayment>();
    }

    public static void AddEventConsumers(this IRegistrationConfigurator cfg)
    {
        cfg.AddConsumer<ProjectPaymentDetailsWhenPaymentChangedConsumer>();
        cfg.AddConsumer<ProceedWithPaymentWhenPaymentRequestedConsumer>();
        cfg.AddConsumer<RequestPaymentWhenOrderPlacedConsumer>();
    }

    public static void AddQueryConsumers(this IRegistrationConfigurator cfg)
    {
        cfg.AddConsumer<GetPaymentDetailsConsumer>();
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