using Application.Services;
using Application.UseCases.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services.AddScoped<IApplicationService, ApplicationService>();
    
    public static IServiceCollection AddCommandInteractors(this IServiceCollection services)
        => services;

    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services
            .AddScoped<IPlaceOrderWhenCartSubmittedInteractor, PlaceOrderWhenCartSubmittedInteractor>()
            .AddScoped<IConfirmOrderWhenPaymentCompletedInteractor, ConfirmOrderWhenPaymentCompletedInteractor>();
}