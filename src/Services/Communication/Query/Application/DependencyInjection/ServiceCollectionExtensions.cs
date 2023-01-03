using Application.Abstractions;
using Application.UseCases.Events;
using Application.UseCases.Queries;
using Contracts.Abstractions.Paging;
using Contracts.Services.Communication;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventInteractors(this IServiceCollection services)
        => services
            .AddScoped<IProjectNotificationDetailsWhenNotificationChangedInteractor, ProjectNotificationDetailsWhenNotificationChangedInteractor>();

    public static IServiceCollection AddQueryInteractors(this IServiceCollection services)
        => services
            .AddScoped<IInteractor<Query.ListNotificationsDetails, IPagedResult<Projection.NotificationDetails>>, ListNotificationsDetailsInteractor>();
}