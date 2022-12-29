using Contracts.Abstractions.Messages;
using Contracts.Services.Communication;
using Domain.Abstractions.Aggregates;
using Domain.Entities;
using Domain.Enumerations;

namespace Domain.Aggregates;

public class Notification : AggregateRoot<NotificationValidator>
{
    private readonly List<NotificationMethod> _methods = new();

    public IEnumerable<NotificationMethod> Methods
        => _methods;

    public override void Handle(ICommand command)
        => Handle(command as dynamic);

    private void Handle(Command.RequestNotification cmd)
        => RaiseEvent(new DomainEvent.NotificationRequested(Guid.NewGuid(), cmd.Methods));

    private void Handle(Command.SendNotificationMethod cmd)
    {
        if (_methods.SingleOrDefault(method => method.Id == cmd.MethodId) is not { IsDeleted: false } notificationMethod) return;
        if (notificationMethod.Status != NotificationMethodStatus.Pending) return;
        RaiseEvent(new DomainEvent.NotificationMethodSent(cmd.NotificationId, cmd.MethodId));
    }

    private void Handle(Command.FailNotificationMethod cmd)
        => RaiseEvent(new DomainEvent.NotificationMethodFailed(cmd.NotificationId, cmd.MethodId));

    private void Handle(Command.CancelNotificationMethod cmd)
        => RaiseEvent(new DomainEvent.NotificationMethodCancelled(cmd.NotificationId, cmd.MethodId));

    private void Handle(Command.ResetNotificationMethod cmd)
        => RaiseEvent(new DomainEvent.NotificationMethodReset(cmd.NotificationId, cmd.MethodId));

    protected override void Apply(IEvent @event)
        => Apply(@event as dynamic);

    private void Apply(DomainEvent.NotificationRequested @event)
    {
        Id = @event.NotificationId;
        _methods.AddRange(@event.Methods.Select(method => (NotificationMethod)method));
    }

    private void Apply(DomainEvent.NotificationMethodSent @event)
        => _methods.First(m => m.Id == @event.MethodId).Send();

    private void Apply(DomainEvent.NotificationMethodFailed @event)
        => _methods.First(m => m.Id == @event.MethodId).Fail();

    private void Apply(DomainEvent.NotificationMethodCancelled @event)
        => _methods.First(m => m.Id == @event.MethodId).Cancel();

    private void Apply(DomainEvent.NotificationMethodReset @event)
        => _methods.First(m => m.Id == @event.MethodId).Reset();
}