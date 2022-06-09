namespace Contracts.Abstractions.Validations;

public record BusinessValidationResult<TMessage>(TMessage Message, IEnumerable<string> Errors) : ValidationResult<TMessage>(Message, Errors);