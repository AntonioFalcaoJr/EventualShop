using FluentValidation.Results;

namespace Messages.Abstractions.Validations;

public record ValidationResultMessage<TMessage>(TMessage Message, ValidationResult ValidationResult);