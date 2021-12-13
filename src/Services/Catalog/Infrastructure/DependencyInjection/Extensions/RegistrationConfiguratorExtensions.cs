using System;
using Application.UseCases.Commands;
using Application.UseCases.Events;
using Application.UseCases.Queries;
using ECommerce.Abstractions.Commands;
using ECommerce.Contracts.Catalog;
using MassTransit;

namespace Infrastructure.DependencyInjection.Extensions;

public static class RegistrationConfiguratorExtensions
{
    public static void AddCommandConsumers(this IRegistrationConfigurator cfg)
    {
        cfg.AddCommandConsumer<DeleteCatalogConsumer>();
        cfg.AddCommandConsumer<UpdateCatalogConsumer>();
        cfg.AddCommandConsumer<CreateCatalogConsumer>();
        cfg.AddCommandConsumer<ActivateCatalogConsumer>();
        cfg.AddCommandConsumer<DeactivateCatalogConsumer>();
        cfg.AddCommandConsumer<AddCatalogItemConsumer>();
        cfg.AddCommandConsumer<RemoveCatalogItemConsumer>();
        cfg.AddCommandConsumer<UpdateCatalogItemConsumer>();
    }

    public static void AddEventConsumers(this IRegistrationConfigurator cfg)
    {
        cfg.AddConsumer<ProjectCatalogDetailsWhenCatalogChangedConsumer>();
    }

    public static void AddQueryConsumers(this IRegistrationConfigurator cfg)
    {
        cfg.AddConsumer<GetCatalogItemsDetailsConsumer>();
    }

    private static void AddCommandConsumer<TConsumer>(this IRegistrationConfigurator cfg)
        where TConsumer : class, IConsumer
        => cfg
            .AddConsumer<TConsumer>()
            .Endpoint(endpoint => endpoint.ConfigureConsumeTopology = false);
}