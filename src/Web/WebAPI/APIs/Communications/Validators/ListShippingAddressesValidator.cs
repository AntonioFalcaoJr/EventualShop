using FluentValidation;

namespace WebAPI.APIs.Communications.Validators;

public class ListEmailsValidator : AbstractValidator<Requests.ListEmails>
{
    public ListEmailsValidator()
    {
        RuleFor(request => request.Limit)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);

        RuleFor(request => request.Offset)
            .GreaterThan(0);
    }
}