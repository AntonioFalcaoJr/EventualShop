using Domain.Abstractions.Entities;
using Domain.Enumerations;
using Domain.ValueObject;

namespace Domain.Entities;

public class NotificationMethod : Entity<NotificationMethodValidator>
{
    public NotificationMethod(Guid id, NotificationOption option)
    {
        Id = id;
        Option = option;
        Status = NotificationMethodStatus.Ready;
    }

    public NotificationOption Option { get; }
    public NotificationMethodStatus Status { get; private set; }

    public void Complete()
        => Status = NotificationMethodStatus.Completed;

    public void Fail()
        => Status = NotificationMethodStatus.Failed;

    public void Cancel()
        => Status = NotificationMethodStatus.Canceled;

    public void Reset()
        => Status = NotificationMethodStatus.Ready;
}