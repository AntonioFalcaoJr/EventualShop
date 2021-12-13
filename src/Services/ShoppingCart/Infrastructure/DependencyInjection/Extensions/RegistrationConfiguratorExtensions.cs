using Application.UseCases.Commands;
using Application.UseCases.Events.Integrations;
using Application.UseCases.Events.Projections;
using Application.UseCases.Queries;
using MassTransit;

namespace Infrastructure.DependencyInjection.Extensions;

public static class RegistrationConfiguratorExtensions
{
    public static void AddCommandConsumers(this IRegistrationConfigurator cfg)
    {
        cfg.AddCommandConsumer<AddCartItemConsumer>();
        cfg.AddCommandConsumer<CreateCartConsumer>();
        cfg.AddCommandConsumer<RemoveCartItemConsumer>();
        cfg.AddCommandConsumer<ChangeBillingAddressConsumer>();
        cfg.AddCommandConsumer<AddShippingAddressConsumer>();
        cfg.AddCommandConsumer<AddCreditCardConsumer>();
        cfg.AddCommandConsumer<AddPayPalConsumer>();
        cfg.AddCommandConsumer<CheckOutCartConsumer>();
        cfg.AddCommandConsumer<UpdateCartItemQuantityConsumer>();
    }

    public static void AddEventConsumers(this IRegistrationConfigurator cfg)
    {
        cfg.AddConsumer<ProjectCartDetailsWhenCartChangedConsumer>();
        cfg.AddConsumer<PublishCartSubmittedWhenCartCheckedOutConsumer>();
    }

    public static void AddQueryConsumers(this IRegistrationConfigurator cfg)
    {
        cfg.AddConsumer<GetShoppingCartConsumer>();
    }

    private static void AddCommandConsumer<TConsumer>(this IRegistrationConfigurator cfg)
        where TConsumer : class, IConsumer
        => cfg
            .AddConsumer<TConsumer>()
            .Endpoint(endpoint => endpoint.ConfigureConsumeTopology = false);
}