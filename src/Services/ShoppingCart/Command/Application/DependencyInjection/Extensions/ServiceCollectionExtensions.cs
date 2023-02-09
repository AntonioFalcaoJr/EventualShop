using Application.Abstractions;
using Application.Services;
using Application.UseCases.Commands;
using Application.UseCases.Events;
using Contracts.Services.ShoppingCart;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services.AddScoped<IApplicationService, ApplicationService>();

    public static IServiceCollection AddCommandInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Command.AddBillingAddress>, AddBillingAddressInteractor>()
            .AddScoped<IInteractor<Command.AddCartItem>, AddCartItemInteractor>()
            .AddScoped<IInteractor<Command.AddCreditCard>, AddCreditCardInteractor>()
            .AddScoped<IInteractor<Command.AddDebitCard>, AddDebitCardInteractor>()
            .AddScoped<IInteractor<Command.AddPayPal>, AddPayPalInteractor>()
            .AddScoped<IInteractor<Command.AddShippingAddress>, AddShippingAddressInteractor>()
            .AddScoped<IInteractor<Command.ChangeCartItemQuantity>, ChangeCartItemQuantityInteractor>()
            .AddScoped<IInteractor<Command.CheckOutCart>, CheckOutCartInteractor>()
            .AddScoped<IInteractor<Command.CreateCart>, CreateCartInteractor>()
            .AddScoped<IInteractor<Command.DiscardCart>, DiscardCartInteractor>()
            .AddScoped<IInteractor<Command.RemoveCartItem>, RemoveCartItemInteractor>()
            .AddScoped<IInteractor<Command.RemovePaymentMethod>, RemovePaymentMethodInteractor>();

    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services
            .AddScoped<IPublishCartSubmittedWhenCartCheckedOutInteractor, PublishCartSubmittedWhenCartCheckedOutInteractor>();
}