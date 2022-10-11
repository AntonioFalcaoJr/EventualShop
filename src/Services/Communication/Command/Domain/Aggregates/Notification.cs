using System.Collections.ObjectModel;
using Contracts.Abstractions.Messages;
using Contracts.Services.Communication;
using Domain.Abstractions.Aggregates;
using Domain.Entities;
using Domain.ValueObject;

namespace Domain.Aggregates;

public class Notification : AggregateRoot<NotificationValidator>
{
    private readonly List<NotificationMethod> _methods = new();

    public IEnumerable<NotificationMethod> Methods
        => _methods;

    public override void Handle(ICommand command)
        => Handle(command as dynamic);

    private void Handle(Command.RequestEmailConfirmation cmd)
        => RaiseEvent(new DomainEvent.EmailConfirmationRequested(cmd.Id, Guid.NewGuid(), cmd.Email));

    protected override void Apply(IEvent @event)
        => Apply(@event as dynamic);

    private void Apply(DomainEvent.EmailConfirmationRequested @event)
    {
        Id = @event.Id;
        _methods.Add(new(@event.MethodId, (Email)@event.Email));
    }
}