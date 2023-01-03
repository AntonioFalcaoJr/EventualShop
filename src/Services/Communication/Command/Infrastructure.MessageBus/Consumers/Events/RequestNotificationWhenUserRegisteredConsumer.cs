using Application.UseCases.Events;
using Infrastructure.MessageBus.Abstractions;
using Contracts.Services.Identity;

namespace Infrastructure.MessageBus.Consumers.Events;

public class RequestNotificationWhenUserRegisteredConsumer : Consumer<DomainEvent.UserRegistered>
{
    public RequestNotificationWhenUserRegisteredConsumer(IRequestNotificationWhenUserRegisteredInteractor interactor) 
        : base(interactor) { }
}