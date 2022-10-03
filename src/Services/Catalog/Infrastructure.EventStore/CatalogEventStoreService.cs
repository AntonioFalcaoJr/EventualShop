using Application.Abstractions.Notifications;
using Application.EventStore;
using Domain.Aggregates;
using Domain.StoreEvents;
using Infrastructure.EventStore.Abstractions;
using Infrastructure.EventStore.DependencyInjection.Options;
using Infrastructure.EventStore.UnitsOfWork;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Infrastructure.EventStore;

public class CatalogEventStoreService : EventStoreService<Catalog, CatalogStoreEvent, CatalogSnapshot, Guid>, ICatalogEventStoreService
{
    public CatalogEventStoreService(IPublishEndpoint publishEndpoint, ICatalogEventStoreRepository repository, INotificationContext notificationContext, IOptionsSnapshot<EventStoreOptions> options, IUnitOfWork unitOfWork)
        : base(publishEndpoint, repository, notificationContext, options, unitOfWork) { }
}