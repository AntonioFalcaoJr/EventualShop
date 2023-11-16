using Application.UseCases.Events;
using Contracts.Boundaries.Order;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class RequestPaymentWhenOrderPlacedConsumer(IRequestPaymentWhenOrderPlacedInteractor interactor) : Consumer<DomainEvent.OrderPlaced>(interactor);