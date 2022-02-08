using ECommerce.Abstractions.Messages.Queries.Paging;
using MassTransit;

namespace ECommerce.Abstractions.Messages.Queries;

[ExcludeFromTopology]
public abstract record QueryPaging(IPaging Paging) : Query;