using Contracts.Abstractions.Messages;
using Contracts.Services.Communication;
using Domain.Abstractions.Aggregates;
using Domain.Entities;
using Domain.Enumerations;
using Newtonsoft.Json;

namespace Domain.Aggregates;

public class Notification : AggregateRoot<NotificationValidator>
{
    [JsonProperty]
    private readonly List<NotificationMethod> _methods = new();

    public IEnumerable<NotificationMethod> Methods
        => _methods.AsReadOnly();

    public override void Handle(ICommand command)
        => Handle(command as dynamic);

    private void Handle(Command.RequestNotification cmd)
        => RaiseEvent<DomainEvent.NotificationRequested>(version => new(Guid.NewGuid(), cmd.Methods, version));

    private void Handle(Command.SendNotificationMethod cmd)
    {
        if (_methods.SingleOrDefault(method => method.Id == cmd.MethodId) is not { IsDeleted: false } notificationMethod) return;
        if (notificationMethod.Status != NotificationMethodStatus.Pending) return;
        RaiseEvent<DomainEvent.NotificationMethodSent>(version => new(cmd.NotificationId, cmd.MethodId, version));
    }

    private void Handle(Command.FailNotificationMethod cmd)
        => RaiseEvent<DomainEvent.NotificationMethodFailed>(version => new(cmd.NotificationId, cmd.MethodId, version));

    private void Handle(Command.CancelNotificationMethod cmd)
        => RaiseEvent<DomainEvent.NotificationMethodCancelled>(version => new(cmd.NotificationId, cmd.MethodId, version));

    private void Handle(Command.ResetNotificationMethod cmd)
        => RaiseEvent<DomainEvent.NotificationMethodReset>(version => new(cmd.NotificationId, cmd.MethodId, version));

    protected override void Apply(IDomainEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvent.NotificationRequested @event)
    {
        Id = @event.NotificationId;
        _methods.AddRange(@event.Methods.Select(method => (NotificationMethod)method));
    }

    private void When(DomainEvent.NotificationMethodSent @event)
        => _methods.First(m => m.Id == @event.MethodId).Send();

    private void When(DomainEvent.NotificationMethodFailed @event)
        => _methods.First(m => m.Id == @event.MethodId).Fail();

    private void When(DomainEvent.NotificationMethodCancelled @event)
        => _methods.First(m => m.Id == @event.MethodId).Cancel();

    private void When(DomainEvent.NotificationMethodReset @event)
        => _methods.First(m => m.Id == @event.MethodId).Reset();
}