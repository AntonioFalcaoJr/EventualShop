using System.Reflection;
using Application.Abstractions;
using Application.Services;
using Application.UseCases.Checkouts.Commands;
using Application.UseCases.ShoppingCarts.Events;
using Microsoft.Extensions.DependencyInjection;
using Checkout = Contracts.Boundaries.Shopping.Checkout.Command;

namespace Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
        => services
            .AddApplicationServices()
            .AddCheckoutCommandInteractors()
            .AddCheckoutEventInteractors()
            .AddShoppingCommandHandlers()
            .AddShoppingCartEventInteractors();

    private static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services.AddScoped<IApplicationService, ApplicationService>();

    private static IServiceCollection AddCheckoutCommandInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Checkout.AddBillingAddress>, AddBillingAddressInteractor>()
            .AddScoped<IInteractor<Checkout.AddCreditCard>, AddCreditCardInteractor>()
            .AddScoped<IInteractor<Checkout.AddDebitCard>, AddDebitCardInteractor>()
            .AddScoped<IInteractor<Checkout.AddPayPal>, AddPayPalInteractor>()
            .AddScoped<IInteractor<Checkout.AddShippingAddress>, AddShippingAddressInteractor>();

    private static IServiceCollection AddCheckoutEventInteractors(this IServiceCollection services)
        => services;

    private static IServiceCollection AddShoppingCommandHandlers(this IServiceCollection services)
        => services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

    private static IServiceCollection AddShoppingCartEventInteractors(this IServiceCollection services)
        => services.AddScoped<IPublishCartProjectionRebuiltWhenRequestedInteractor, PublishCartProjectionRebuiltWhenRequestedInteractor>();
}