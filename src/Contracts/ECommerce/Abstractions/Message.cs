using MassTransit.Topology;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ECommerce.Abstractions;

[ExcludeFromTopology]
public abstract record Message : IMessage
{
    [BindNever]
    public DateTimeOffset Timestamp { get; private set; } = DateTimeOffset.Now;
}