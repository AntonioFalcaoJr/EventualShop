using FluentValidation;

namespace Domain.Aggregates;

public class NotificationValidator : AbstractValidator<Notification>
{
    public NotificationValidator()
    {
        RuleFor(notification => notification.Id)
            .NotEmpty();
    }
}