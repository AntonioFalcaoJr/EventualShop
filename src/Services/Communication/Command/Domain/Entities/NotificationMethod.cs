using Contracts.DataTransferObjects;
using Domain.Abstractions.Entities;
using Domain.Enumerations;
using Domain.ValueObject;

namespace Domain.Entities;

public class NotificationMethod : Entity<NotificationMethodValidator>
{
    public NotificationMethod(Guid id, INotificationOption option)
    {
        Id = id;
        Option = option;
        Status = NotificationMethodStatus.Pending;
    }

    public INotificationOption Option { get; }
    public NotificationMethodStatus Status { get; private set; }

    public void Send()
        => Status = NotificationMethodStatus.Sent;

    public void Fail()
        => Status = NotificationMethodStatus.Failed;

    public void Cancel()
        => Status = NotificationMethodStatus.Cancelled;

    public void Reset()
        => Status = NotificationMethodStatus.Pending;

    public static implicit operator NotificationMethod(Dto.NotificationMethod method)
        => new(method.MethodId, method.Option switch
        {
            Dto.Email email => (Email)email,
            Dto.Sms sms => (Sms)sms,
            Dto.PushMobile pushMobile => (PushMobile)pushMobile,
            Dto.PushWeb pushWeb => (PushWeb)pushWeb,
            _ => default
        });
}