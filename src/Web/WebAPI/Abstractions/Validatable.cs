using FluentValidation;

namespace WebAPI.Abstractions;

public abstract record Validatable<TValidator>
    where TValidator : IValidator, new()
{
    private readonly TValidator _validator = new();

    public bool IsValid(out IDictionary<string, string[]> errors)
    {
        var result = _validator.Validate(new ValidationContext<Validatable<TValidator>>(this));
        errors = result.ToDictionary();
        return result.IsValid;
    }
}