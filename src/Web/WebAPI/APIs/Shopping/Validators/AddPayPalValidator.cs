namespace WebAPI.APIs.Shopping.Validators;

// public class AddPayPalValidator : AbstractValidator<Commands.AddPayPal>
// {
//     public AddPayPalValidator()
//     {
//         RuleFor(request => request.CartId)
//             .NotEmpty();
//
//         RuleFor(request => request.Payload)
//             .SetValidator(new AddPaypalPayloadValidator())
//             .OverridePropertyName(string.Empty);
//     }
// }