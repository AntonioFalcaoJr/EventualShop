using Application.Abstractions;
using Application.Services;
using Application.UseCases.Events;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart = Contracts.Services.ShoppingCart;
using Payment = Contracts.Services.Payment;

namespace Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services
            .AddScoped<IApplicationService, ApplicationService>();
    
    public static IServiceCollection AddCommandInteractors(this IServiceCollection services)
        => services;

    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<ShoppingCart.IntegrationEvent.CartSubmitted>, CartSubmittedInteractor>()
            .AddScoped<IInteractor<Payment.DomainEvent.PaymentCompleted>, PaymentCompletedInteractor>();
}