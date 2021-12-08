using System;
using Application.UseCases.Commands;
using Application.UseCases.Events;
using Application.UseCases.Queries;
using ECommerce.Abstractions;
using ECommerce.Abstractions.Commands;
using ECommerce.Contracts.Catalog;
using MassTransit;

namespace Infrastructure.DependencyInjection.Extensions;

public static class RegistrationConfiguratorExtensions
{
    public static void AddCommandConsumers(this IRegistrationConfigurator cfg)
    {
        cfg.AddCommandConsumer<DeleteCatalogConsumer, Commands.DeleteCatalog>();
        cfg.AddCommandConsumer<UpdateCatalogConsumer, Commands.UpdateCatalog>();
        cfg.AddCommandConsumer<CreateCatalogConsumer, Commands.CreateCatalog>();
        cfg.AddCommandConsumer<ActivateCatalogConsumer, Commands.ActivateCatalog>();
        cfg.AddCommandConsumer<DeactivateCatalogConsumer, Commands.DeactivateCatalog>();
        cfg.AddCommandConsumer<AddCatalogItemConsumer, Commands.AddCatalogItem>();
        cfg.AddCommandConsumer<RemoveCatalogItemConsumer, Commands.RemoveCatalogItem>();
        cfg.AddCommandConsumer<UpdateCatalogItemConsumer, Commands.UpdateCatalogItem>();
    }

    public static void AddEventConsumers(this IRegistrationConfigurator cfg)
    {
        cfg.AddConsumer<ProjectCatalogDetailsWhenCatalogChangedConsumer>();
    }

    public static void AddQueryConsumers(this IRegistrationConfigurator cfg)
    {
        cfg.AddConsumer<GetCatalogItemsDetailsConsumer>();
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