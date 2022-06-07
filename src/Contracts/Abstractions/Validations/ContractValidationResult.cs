namespace Contracts.Abstractions.Validations;

public record ContractValidationResult<TMessage>(TMessage Message, IEnumerable<string> Errors) : ValidationResult<TMessage>(Message, Errors);