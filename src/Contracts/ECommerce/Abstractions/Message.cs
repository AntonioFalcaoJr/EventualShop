using System;
using MassTransit;
using MassTransit.Topology;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ECommerce.Abstractions;

[ExcludeFromTopology]
public abstract record Message : IMessage
{
    [BindNever]
    public DateTimeOffset Timestamp { get; private init; } = DateTimeOffset.Now;

    [BindNever]
    public Guid CorrelationId { get; private init; } = Guid.NewGuid();
}