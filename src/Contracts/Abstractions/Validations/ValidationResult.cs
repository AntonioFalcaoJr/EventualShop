using MassTransit;

namespace Contracts.Abstractions.Validations;

[ExcludeFromTopology]
public abstract record ValidationResult<TMessage>(TMessage Message, IEnumerable<string> Errors);