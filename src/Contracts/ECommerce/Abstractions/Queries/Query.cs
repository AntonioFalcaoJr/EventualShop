using System;
using MassTransit.Topology;

namespace ECommerce.Abstractions.Queries;

[ExcludeFromTopology]
public abstract record Query(Guid CorrelationId = default) : Message(CorrelationId), IQuery;