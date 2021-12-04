using FluentValidation.Results;

namespace ECommerce.Abstractions.Validations;

public record ValidationResultMessage<TMessage>(TMessage Message, ValidationResult ValidationResult);