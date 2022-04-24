using MassTransit;

namespace ECommerce.Abstractions;

[ExcludeFromTopology]
public abstract record ValidationResult<TMessage>(TMessage Message, IEnumerable<string> Errors);

public record BusinessValidationResult<TMessage>(TMessage Message, IEnumerable<string> Errors) : ValidationResult<TMessage>(Message, Errors);

public record ContractValidationResult<TMessage>(TMessage Message, IEnumerable<string> Errors) : ValidationResult<TMessage>(Message, Errors);