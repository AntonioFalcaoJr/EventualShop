namespace WebAPI.APIs.Shopping.Validators;

// public class AddDebitCardValidator : AbstractValidator<Commands.AddDebitCard>
// {
//     public AddDebitCardValidator()
//     {
//         RuleFor(request => request.CartId)
//             .NotEmpty();
//
//         RuleFor(request => request.Payload)
//             .SetValidator(new AddDebitCardPayloadValidator())
//             .OverridePropertyName(string.Empty);
//     }
// }