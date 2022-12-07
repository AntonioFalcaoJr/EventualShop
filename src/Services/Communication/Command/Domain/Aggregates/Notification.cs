using Contracts.Abstractions.Messages;
using Contracts.Services.Communication;
using Domain.Abstractions.Aggregates;
using Domain.Entities;

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

    protected override void Apply(IEvent @event)
        => Apply(@event as dynamic);

    private void Apply(DomainEvent.NotificationRequested @event)
    {
        Id = @event.NotificationId;
        _methods.AddRange(@event.Methods.Select(method => (NotificationMethod)method));
    }
}