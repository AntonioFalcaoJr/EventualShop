using System;
using Application.UseCases.Commands;
using Application.UseCases.Events;
using Application.UseCases.Queries;
using MassTransit;
using Messages.Abstractions;
using Messages.ShoppingCarts;

namespace Infrastructure.DependencyInjection.Extensions
{
    public static class RegistrationConfiguratorExtensions
    {
        public static void AddCommandConsumers(this IRegistrationConfigurator cfg)
        {
            cfg.AddCommandConsumer<AddCartItemConsumer, Commands.AddCartItem>();
            cfg.AddCommandConsumer<CreateCartConsumer, Commands.CreateCart>();
            cfg.AddCommandConsumer<RemoveCartItemConsumer, Commands.RemoveCartItem>();
        }

        public static void AddEventConsumers(this IRegistrationConfigurator cfg)
        {
            cfg.AddConsumer<CartCreatedConsumer>();
            cfg.AddConsumer<CartItemAddedConsumer>();
            cfg.AddConsumer<CartItemRemovedConsumer>();
        }

        public static void AddQueryConsumers(this IRegistrationConfigurator cfg)
        {
             cfg.AddConsumer<GetShoppingCartConsumer>();
        }

        private static void AddCommandConsumer<TConsumer, TMessage>(this IRegistrationConfigurator configurator)
            where TConsumer : class, IConsumer
            where TMessage : class, IMessage
        {
            configurator
                .AddConsumer<TConsumer>()
                .Endpoint(endpoint => endpoint.ConfigureConsumeTopology = false);

            EndpointConvention.Map<TMessage>(new Uri($"exchange:{typeof(TMessage).ToKebabCaseString()}"));
        }
    }
}