using System;
using Application.UseCases.CommandsHandlers;
using Application.UseCases.EventHandlers;
using Application.UseCases.QueriesHandlers;
using MassTransit;
using Messages.Abstractions;
using Messages.Catalogs;

namespace Infrastructure.DependencyInjection.Extensions
{
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
            cfg.AddConsumer<CatalogCreatedConsumer>();
            cfg.AddConsumer<CatalogChangedConsumer>();
        }

        public static void AddQueryConsumers(this IRegistrationConfigurator cfg)
        {
            cfg.AddConsumer<GetCatalogItemsDetailsConsumer>();
        }

        private static void AddCommandConsumer<TConsumer, TMessage>(this IRegistrationConfigurator configurator)
            where TConsumer : class, IConsumer
            where TMessage : class, IMessage
        {
            configurator
                .AddConsumer<TConsumer>()
                .Endpoint(e => e.ConfigureConsumeTopology = false);

            EndpointConvention.Map<TMessage>(new Uri($"exchange:{typeof(TMessage).ToKebabCaseString()}"));
        }
    }
}