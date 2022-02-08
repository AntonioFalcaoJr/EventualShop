using ECommerce.Abstractions.Messages.Queries.Paging;
using MassTransit.Topology;

namespace ECommerce.Abstractions.Messages.Queries;

[ExcludeFromTopology]
public abstract record QueryPaging(IPaging Paging) : Query;