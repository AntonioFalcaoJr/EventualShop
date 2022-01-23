using System.Collections.Generic;
using MassTransit.Topology;

namespace ECommerce.Abstractions.Validations;

[ExcludeFromTopology]
public abstract record ValidationResultMessage<TMessage>(TMessage Message, IEnumerable<string> Errors);

public record BusinessValidationResult<TMessage>(TMessage Message, IEnumerable<string> Errors) : ValidationResultMessage<TMessage>(Message, Errors);

public record ContractValidationResult<TMessage>(TMessage Message, IEnumerable<string> Errors) : ValidationResultMessage<TMessage>(Message, Errors);