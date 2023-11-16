using Application.Abstractions;
using Application.UseCases.Events;
using Application.UseCases.Queries;
using Contracts.Boundaries.Payment;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services
            .AddScoped<IProjectPaymentMethodDetailsWhenChangedInteractor, ProjectPaymentMethodDetailsWhenChangedInteractor>()
            .AddScoped<IProjectPaymentWhenChangedInteractor, ProjectPaymentWhenChangedInteractor>();

    public static IServiceCollection AddQueryInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Query.GetPaymentDetails, Projection.PaymentDetails>, GetPaymentDetailsInteractor>();
}