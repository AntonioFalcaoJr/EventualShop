using System;
using Application.UseCases.Commands;
using Application.UseCases.EventHandlers;
using Application.UseCases.Queries;
using MassTransit;
using Messages.Catalogs.Commands;

namespace Infrastructure.DependencyInjection.Extensions
{
    public static class BusConfiguratorExtensions
    {
        public static void AddCommandConsumers(this IRegistrationConfigurator cfg)
        {
            cfg.AddCommandConsumer<DeleteCatalogConsumer, DeleteCatalog>();
            cfg.AddCommandConsumer<UpdateCatalogConsumer, UpdateCatalog>();
            cfg.AddCommandConsumer<CreateCatalogConsumer, CreateCatalog>();
            cfg.AddCommandConsumer<ActivateCatalogConsumer, ActivateCatalog>();
            cfg.AddCommandConsumer<DeactivateCatalogConsumer, DeactivateCatalog>();
            cfg.AddCommandConsumer<AddCatalogItemConsumer, AddCatalogItem>();
            cfg.AddCommandConsumer<RemoveCatalogItemConsumer, RemoveCatalogItem>();
        }

        public static void AddEventConsumers(this IRegistrationConfigurator cfg)
        {
            cfg.AddConsumer<CatalogCreatedConsumer>();
            cfg.AddConsumer<CatalogChangedConsumer>();
            cfg.AddConsumer<CatalogChangedConsumer>();
        }

        public static void AddQueryConsumers(this IRegistrationConfigurator cfg)
        {
            cfg.AddConsumer<GetCatalogItemsDetailsConsumer>();
        }

        private static void AddCommandConsumer<TConsumer, TMessage>(this IRegistrationConfigurator configurator)
            where TConsumer : class, IConsumer
            where TMessage : class
        {
            configurator
                .AddConsumer<TConsumer>()
                .Endpoint(e => e.ConfigureConsumeTopology = false);

            MapQueueEndpoint<TMessage>();
        }

        private static void MapQueueEndpoint<TMessage>()
            where TMessage : class
            => EndpointConvention.Map<TMessage>(new Uri($"exchange:{typeof(TMessage).ToKebabCaseString()}"));
    }
}