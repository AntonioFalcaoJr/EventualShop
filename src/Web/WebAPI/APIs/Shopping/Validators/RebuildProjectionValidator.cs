using FluentValidation;

namespace WebAPI.APIs.Shopping.Validators;

public class RebuildProjectionValidator : AbstractValidator<Commands.RebuildProjection>
{
    public RebuildProjectionValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty();
    }
}