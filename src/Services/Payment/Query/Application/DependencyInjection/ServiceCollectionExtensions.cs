using Application.Abstractions;
using Application.UseCases.Events;
using Application.UseCases.Queries;
using Contracts.Services.Payment;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services
            .AddScoped<IProjectPaymentMethodWhenChangedInteractor, ProjectPaymentMethodWhenChangedInteractor>()
            .AddScoped<IProjectPaymentWhenChangedInteractor, ProjectPaymentWhenChangedInteractor>();

    public static IServiceCollection AddQueryInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Query.GetPayment, Projection.Payment>, GetPaymentInteractor>();
}