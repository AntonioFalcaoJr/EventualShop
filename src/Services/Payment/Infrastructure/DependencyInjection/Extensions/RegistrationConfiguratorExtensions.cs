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
        cfg.AddCommandConsumer<RequestPaymentConsumer>();
        cfg.AddCommandConsumer<CancelPaymentConsumer>();
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

    private static void AddCommandConsumer<TConsumer>(this IRegistrationConfigurator cfg)
        where TConsumer : class, IConsumer
        => cfg
            .AddConsumer<TConsumer>()
            .Endpoint(endpoint => endpoint.ConfigureConsumeTopology = false);
}